using AutoMapper;
using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.YooKassaPayment.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace KartoshkaEvent.YooKassaPayment
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddYooKassaService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PaymentProfile));
            services.AddAutoMapper(typeof(FullRefundProfile));


            services
                .AddScoped<IYooKassaService>(provider =>
                {
                    var templateService = provider.GetRequiredService<IMessageTemplateService>();
                    var mapper = provider.GetRequiredService<IMapper>();

                    return new YooKassaService("1091244", "test_-NzQ_gwqFk3TDDtrxdML_zKWyIy-MzRvoWqyXkRbZTI", templateService, mapper);
                });

            return services;
        }
    }
}
