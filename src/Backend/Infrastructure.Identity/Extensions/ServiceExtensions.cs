using System;
using EvrenDev.Application.DTOS.Settings;
using EvrenDev.Application.Interfaces;
using EvrenDev.Infrastructure.Identity.Services;
using EvrenDev.Infrastructure.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EvrenDev.Infrastructure.Identity.Extensions {
    public static class ServiceExtensions 
    {
        public static void AddIdentitytInfrastructure(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services
                .AddDbContext<IdentityContext>(options =>
                    options
                        .UseSqlServer(configuration.GetConnectionString(name: "IdentityConnection"))
                );
                
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    // Signin Options
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedPhoneNumber = true;

                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()_+|~-={}[]:\";<>?,./";
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            #region Services

            services.AddTransient<IIdentityService, IdentityService>();

            #endregion Services
            
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.SaveToken = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
        }
    }
}