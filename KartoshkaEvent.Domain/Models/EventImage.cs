namespace KartoshkaEvent.Domain.Models
{
    public record EventImage
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}