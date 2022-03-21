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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using EvrenDev.Application.Interfaces.Result;
using EvrenDev.Application.Exceptions;
using System.Threading.Tasks;

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
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
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
                .AddDefaultTokenProviders()
                .AddTokenProvider("EvrenDevPublicApi", typeof(DataProtectorTokenProvider<ApplicationUser>));

            services.AddTransient<IIdentityService, IdentityService>();
            
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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

                option.Events = new JwtBearerEvents()
                {
                       OnAuthenticationFailed = context =>
                       {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return System.Threading.Tasks.Task.CompletedTask;
                       },
                    //    OnChallenge = context =>
                    //    {
                    //        context.HandleResponse();

                    //        if(!context.Response.HasStarted) {
                    //            context.Response.StatusCode = 401;
                    //            context.Response.ContentType = "application/json";
                    //            var result = JsonConvert.SerializeObject(Result<string>.Fail("You are not Authorized"));
                    //            return context.Response.WriteAsync(result);
                    //        } 
                           
                    //        return Task.CompletedTask;      
                    //    },
                    //    OnForbidden = context =>
                    //    {
                    //        context.Response.StatusCode = 403;
                    //        context.Response.ContentType = "application/json";
                    //        var result = JsonConvert.SerializeObject(Result<string>.Fail("You are not authorized to access this resource"));
                    //        return context.Response.WriteAsync(result);
                    //    },
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
                        builder.WithExposedHeaders("Token-Expired");
                    });
            });
        }
    }
}