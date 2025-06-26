using KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Search.BetweenDate;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Search.GetByMood
{
    public class EventGetByMood
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }


        public OwnerGetByMood OwnerInfo { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
        public List<LocationGetByMood> Locations { get; set; }
    }
}
