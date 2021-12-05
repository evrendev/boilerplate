using System.Reflection;
using EvrenDev.Api.Validators.Settings.Content;
using EvrenDev.Api.Validators.Settings.Department;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.DTOS.User;
using EvrenDev.Application.Features.Settings.Department.Commands.Create;
using EvrenDev.Application.MappingProfile;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EvrenDev.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper();

            Mapper.Initialize(cfg => {
                cfg.AddProfile<ContentProfile>();
                cfg.AddProfile<DepartmentProfile>();
                cfg.AddProfile<ApplicationUserProfile>();
            });
            
            services.AddFluentValidation(fv =>
            {
                ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;
            });

            //ApplicationUser Validators
            services.AddTransient<IValidator<CreateApplicationUserCommand>, CreateApplicationUserValidator>();
            services.AddTransient<IValidator<UpdateApplicationUserCommand>, UpdateApplicationUserValidator>();
            services.AddTransient<IValidator<DeleteApplicationUserCommand>, DeleteApplicationUserValidator>();
            
            //Department Validators
            services.AddTransient<IValidator<CreateDepartmentCommand>, CreateDepartmentValidator>();
            services.AddTransient<IValidator<UpdateDepartmentCommand>, UpdateDepartmentValidator>();
            services.AddTransient<IValidator<DeleteDepartmentCommand>, DeleteDepartmentValidator>();
            
            //Content Validators
            services.AddTransient<IValidator<CreateContentCommand>, CreateContentValidator>();
            services.AddTransient<IValidator<UpdateContentCommand>, UpdateContentValidator>();
            services.AddTransient<IValidator<DeleteContentCommand>, DeleteContentValidator>();
            
            services.AddMediatR(typeof(CreateDepartmentCommandHandler).GetTypeInfo().Assembly);
        }
    }
}