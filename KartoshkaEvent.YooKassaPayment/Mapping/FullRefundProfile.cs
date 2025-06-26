using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.FullRefund;
using KartoshkaEvent.Application.Features.Commands.Tickets.FullRefund;
using KartoshkaEvent.YooKassaPayment.Models.FullRefund;

namespace KartoshkaEvent.YooKassaPayment.Mapping
{
    public class FullRefundProfile : Profile
    {
        public FullRefundProfile()
        {
            CreateMap<FullRefund, FullRefundDto>();
        }
    }
}
