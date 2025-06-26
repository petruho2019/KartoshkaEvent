using AutoMapper;
using KartoshkaEvent.Application.Common.Mappings;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KartoshkaEvent.Application.Features.Commands.Events.Edit
{
    public class EditEventCommand : IRequest<Result>, IMapWith<Event>
    {
        [Required(ErrorMessage = "Идентификатор мероприятия обязателен")]
        public Guid EventId { get; set; }
        [Required(ErrorMessage = "Идентификатор места проведения обязателен")]
        public Guid AddressId { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public EventType EventType { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int NumberOfHouse { get; set; }
        public long QuantityOfTickets { get; set; }
        public decimal PriceOfTickets { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditEventCommand, Event>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            profile.CreateMap<EditEventCommand, Location>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AddressId))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.NumberOfhouse, opt => opt.MapFrom(src => src.NumberOfHouse));

            profile.CreateMap<EditEventCommand, TimeOfEvent>()
                .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src.DateStart))
                .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src.DateEnd));

            profile.CreateMap<EditEventCommand, TicketInfo>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.QuantityOfTickets));

        }

    }
}
