using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Visitor.GetHistory
{
    public class EventHistoryDto
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        

        public OwnerHistoryDto OwnerInfo { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Images { get; set; }
        public List<LocationHsitoryDto> Location { get; set; }
    }
}
