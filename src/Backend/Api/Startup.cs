using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EvrenDev.Api.Extensions;
using EvrenDev.Application.Extensions;
using EvrenDev.Infrastructure.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace EvrenDev.Api
{
    public class Startup
    {   
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddContextInfrastructure(_configuration);
            services.AddPersistenceContexts();
            services.AddRepositories();
            services.AddSharedInfrastructure(_configuration);
            services.AddEssentials();
            services.AddControllers();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"))
            };
            app.UseRequestLocalization(options);

            app.UseMiddleware<LocalizationMiddleware>();

            app.ConfigureSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();
            
            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                }
            );
        }
    }
}