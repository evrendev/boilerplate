using EvrenDev.Application.Interfaces.Repositories;
using EvrenDev.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EvrenDev.Infrastructure.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistencInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationContext>(options =>
                    options
                        .UseSqlServer(configuration.GetConnectionString(name: "ApplicationConnection"))
                );

            #region Repositories

            services
                .AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>))
                .AddScoped<IContentRepository, ContentRepository>();

            #endregion Repositories
        }
    }
}