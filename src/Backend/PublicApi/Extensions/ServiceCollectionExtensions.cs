using EvrenDev.Application.Extensions;
using EvrenDev.Application.Interfaces.Repositories;
using EvrenDev.Application.Interfaces.Shared;
using EvrenDev.Application.Localization;
using EvrenDev.Application.Logging;
using EvrenDev.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace EvrenDev.PublicApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services
                .AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>))
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddHealthChecks();
        }

        public static void AddEssentials(this IServiceCollection services)
        {
            services.AddLocalizationSupport();
            services.AddRouing();
            services.RegisterSwagger();
            services.AddVersioning();
        }

        private static void AddLocalizationSupport(this IServiceCollection services)
        {
            services.AddLocalization();
            services.AddSingleton<LocalizationMiddleware>();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        }

        private static void AddRouing(this IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddNewtonsoftJson(jsonOptions => 
                jsonOptions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        private static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "EvrenDev Public Api",
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        private static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
}
}