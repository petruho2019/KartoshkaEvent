using KartoshkaEvent.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KartoshkaEvent.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services
            , IConfiguration configuration)
        {
            var connectionString =
                configuration["DataAccess:ConnectionStringDefault"] ?? throw new ArgumentNullException(nameof(configuration), "ConnectionString is missing in configuration.");

            services.AddDbContext<KartoshkaEventContext>(opt =>
            {
                opt.UseNpgsql(connectionString, opt =>
                {
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            });
            services.AddScoped<IKartoshkaEventContext, KartoshkaEventContext>();

            return services;
        }
    }
}
