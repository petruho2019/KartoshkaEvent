
namespace KartoshkaEvent.Domain.Models
{
    public record Event
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }

        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public List<EventImage> Images { get; set; } // TODO Изображения у мероприятий на разных адресах может отличаться?
        public List<Location> LocationsOfEvents { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
