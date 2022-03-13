using EvrenDev.Application.Interfaces.Shared;
using EvrenDev.Infrastructure.Persistence.Services;
using EvrenDev.Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EvrenDev.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>()
                .AddTransient<IDateTimeService, SystemDateTimeService>();
        }
    }
}
