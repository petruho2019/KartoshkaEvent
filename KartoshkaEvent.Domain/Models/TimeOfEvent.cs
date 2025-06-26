namespace KartoshkaEvent.Domain.Models
{
    public record TimeOfEvent
    {
        public Guid Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }
    }
}
