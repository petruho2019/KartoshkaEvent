using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.Buy;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Mapping
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Location, TicketPaymentObject>()
                .ForMember(dest => dest.PriceOfTicket, opt => opt.MapFrom(src => src.TicketInfo.Price))
                .ForMember(dest => dest.QuantityOfTickets, opt => opt.MapFrom(src => src.TicketInfo.Quantity));
        }
    }
}
