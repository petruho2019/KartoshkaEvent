using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Mapping
{
    public class FullRefundTicketProfile : Profile
    {
        public FullRefundTicketProfile()
        {
            CreateMap<Ticket, FullRefundTicketDto>()
                .ForPath(dest => dest.LocationInfo.City, opt => opt.MapFrom(src => src.EventLocation.City))
                .ForPath(dest => dest.LocationInfo.Street, opt => opt.MapFrom(src => src.EventLocation.Street))
                .ForPath(dest => dest.LocationInfo.NumberOfHouse, opt => opt.MapFrom(src => src.EventLocation.NumberOfhouse))
                .ForPath(dest => dest.EventInfo.Subject, opt => opt.MapFrom(src => src.Event.Subject))
                .ForPath(dest => dest.TicketInfo.TotalPrice, opt => opt.MapFrom(src => src.PriceByTicket * src.TotalQuantity))
                .ForPath(dest => dest.TicketInfo.Quantity, opt => opt.MapFrom(src => src.TotalQuantity))
                .ForPath(dest => dest.TicketInfo.PaymentId, opt => opt.MapFrom(src => src.PaymentId));

        }
    }
}
