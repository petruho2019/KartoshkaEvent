using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetHistory;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Organizer.GetMyCreatedEvents
{
    public class CreatedEventDto
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }

        public List<string> Tags { get; set; }
        public List<string> Images { get; set; }
        public List<CreatedLocationDto> Location { get; set; }
    }
}
