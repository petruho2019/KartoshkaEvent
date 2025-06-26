using KartoshkaEvent.Application.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.JwtProvider
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJwtProvider(this IServiceCollection services)
        {

            services.AddTransient<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}