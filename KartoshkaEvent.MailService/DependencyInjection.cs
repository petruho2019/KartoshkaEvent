using KartoshkaEvent.Application.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KartoshkaEvent.MailService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMailService(this IServiceCollection services)
        {

            services.AddTransient<IMailService, MailService>();

            return services;
        }
    }
}