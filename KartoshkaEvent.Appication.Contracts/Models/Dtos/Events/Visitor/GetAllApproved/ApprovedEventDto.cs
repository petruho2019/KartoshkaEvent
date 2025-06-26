using AutoMapper;
using KartoshkaEvent.Application.Common.Mappings;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetAllApproved
{
    public class ApprovedEventDto : IMapWith<Event>
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }

        public Guid OwnerId { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
        public List<ApprovedLocationDto> Locations { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, ApprovedEventDto>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType.ToString()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImagePath)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name.ToString())))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.LocationsOfEvents));
        }
    }
}
