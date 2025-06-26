namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Moderation.Events.GetAllForModeration
{
    public class ModerationEventDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }

        public Guid OwnerId { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
        public List<ModerationLocationEventDto> Addresses { get; set; }
    }
}
