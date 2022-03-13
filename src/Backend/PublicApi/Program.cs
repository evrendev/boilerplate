using Loggly;
using Loggly.Config;
using EvrenDev.Application.Settings;
using EvrenDev.Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using EvrenDev.Infrastructure.Identity.Model;
using EvrenDev.Infrastructure.Persistence;

namespace EvrenDev.PublicApi
{
    public class Program
    {
        private static string _environmentName;
        public static async Task Main(string[] args)
        {
            var webHost = BuildWebHost(args);

            //read configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{_environmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            //Must have Loggly account and setup correct info in appsettings
            if (configuration["Serilog:UseLoggly"] == "true")
            {
                var logglySettings = new LogglySettings();
                configuration.GetSection("Serilog:Loggly").Bind(logglySettings);
                SetupLogglyConfiguration(logglySettings);
            }

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            using (var scope = webHost.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database.Migrate();
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var dbContext = services.GetService<ApplicationContext>();

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaultUsers.SeedAsync(userManager, roleManager, loggerFactory);
                    await DefaultSuperAdminUser.SeedAsync(userManager, roleManager, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            //Start webHost
            try
            {
                Log.Information("Starting web host");
                await webHost.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, config) =>
                {
                    config.ClearProviders(); //Disabling default integrated logger
                    _environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                })
                .UseStartup<Startup>()
                .UseSerilog() // <-- Add this line
                .Build();


        /// <summary>
        /// Configure Loggly provider
        /// </summary>
        /// <param name="logglySettings"></param>
        private static void SetupLogglyConfiguration(LogglySettings logglySettings)
        {
            //Configure Loggly
            var config = LogglyConfig.Instance;
            config.CustomerToken = logglySettings.CustomerToken;
            config.ApplicationName = logglySettings.ApplicationName;
            config.Transport = new TransportConfiguration()
            {
                EndpointHostname = logglySettings.EndpointHostname,
                EndpointPort = logglySettings.EndpointPort,
                LogTransport = logglySettings.LogTransport
            };
            config.ThrowExceptions = logglySettings.ThrowExceptions;

            //Define Tags sent to Loggly
            config.TagConfig.Tags.AddRange(new ITag[]
            {
                new ApplicationNameTag {Formatter = "Application-{0}"},
                new HostnameTag {Formatter = "Host-{0}"}
            });
        }
    }
}