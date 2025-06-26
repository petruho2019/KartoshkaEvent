using KartoshkaEvent.Application.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KartoshkaEvent.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationContracts(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(opt
                => opt.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));

            services.AddAutoMapper(opt
                => opt.AddProfile(new AssemblyMappingProfile(assembly)));

            return services;
        }
    }
}
