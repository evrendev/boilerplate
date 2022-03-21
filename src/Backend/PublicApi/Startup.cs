using EvrenDev.PublicApi.Extensions;
using EvrenDev.Application.Extensions;
using EvrenDev.Infrastructure.Persistence.Extensions;
using EvrenDev.Infrastructure.Shared;
using EvrenDev.Infrastructure.Identity.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;
using EvrenDev.Infrastructure.Identity.Model;

namespace EvrenDev.PublicApi
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
            services.AddIdentitytInfrastructure(_configuration);
            services.AddPersistencInfrastructure(_configuration);
            services.AddSharedInfrastructure();
            services.AddApplicationLayer(_configuration);
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
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en-US"))
            };
            
            app.UseRequestLocalization(options);

            app.UseMiddleware<LocalizationMiddleware>();

            app.ConfigureSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            
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