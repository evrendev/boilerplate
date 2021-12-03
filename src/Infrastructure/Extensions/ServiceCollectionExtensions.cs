using EvrenDev.Application.Interfaces.Repositories;
using EvrenDev.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EvrenDev.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services
                .AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>))
                .AddScoped<IDepartmentRepository, DepartmentRepository>()
                .AddScoped<IContentRepository, ContentRepository>();

            #endregion Repositories
        }
    }
}