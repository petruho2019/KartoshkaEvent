using KartoshkaEvent.Application.Common;
using KartoshkaEvent.Application.Common.RegulareExpressions;
using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Application.Mapping;
using KartoshkaEvent.Application.Services;
using KartoshkaEvent.Application.Services.Confirmation;
using KartoshkaEvent.Application.Services.Cookie;
using KartoshkaEvent.Application.Services.Image;
using KartoshkaEvent.Application.Services.Locations;
using KartoshkaEvent.Application.Services.Mail;
using KartoshkaEvent.Application.Services.Qr;
using KartoshkaEvent.Application.Services.Tags;
using KartoshkaEvent.Application.Services.Tokens;
using KartoshkaEvent.Application.Services.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.RegularExpressions;

namespace KartoshkaEvent.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddApplicationContracts(Assembly.GetExecutingAssembly());
          
            services.AddMediatR(conf
                => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            AddServices(services);
            
            services.AddScoped<CurrentUserService>();

            services.AddAutoMapper(typeof(LocationProfile));
            services.AddAutoMapper(typeof(FullRefundTicketProfile));

            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            foreach (var serviceType in Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                IsServiceClassRegex.IsService(t.Name)
                &&
                t.GetInterfaces().Any(i => IsServiceInterfaceRegex.IsServiceInterface(i.Name))
                ))
            {
                services.AddScoped(serviceType.GetInterfaces().First(i => IsServiceInterfaceRegex.IsServiceInterface(i.Name)), serviceType);
            }
        }


    }
}
