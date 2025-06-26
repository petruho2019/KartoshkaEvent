using AutoMapper;
using KartoshkaEvent.Application.Common.Mappings;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.Buy
{
    public class TicketPaymentObject : IMapWith<PaymentInfoCacheDto>
    {
        public string BuyerEmail { get; set; }

        public long QuantityOfTickets { get; set; }
        public decimal PriceOfTicket { get; set; }
        public string SubjectOfEvent { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int NumberOfHouse { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TicketPaymentObject, PaymentInfoCacheDto>()
                .ForMember(dest => dest.BuyerEmail, opt => opt.MapFrom(src => src.BuyerEmail))
                .ForPath(dest => dest.TicketInfo.PriceOfTickets, opt => opt.MapFrom(src => src.PriceOfTicket * src.QuantityOfTickets))
                .ForPath(dest => dest.TicketInfo.QuantityOfTickets, opt => opt.MapFrom(src => src.QuantityOfTickets))
                .ForPath(dest => dest.LocationInfo.City, opt => opt.MapFrom(src => src.City))
                .ForPath(dest => dest.LocationInfo.Street, opt => opt.MapFrom(src => src.Street))
                .ForPath(dest => dest.LocationInfo.NumberOfHouse, opt => opt.MapFrom(src => src.NumberOfHouse))
                .ForPath(dest => dest.EventInfo.Subject, opt => opt.MapFrom(src => src.SubjectOfEvent));
        }
    }
}
