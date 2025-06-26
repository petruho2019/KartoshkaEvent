namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Events.Search.BetweenDate
{
    public class EventBetweenDateDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }


        public OwnerBetweenDateDto OwnerInfo { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
        public List<LocationBetweenDateDto> Locations { get; set; }
    }
}
