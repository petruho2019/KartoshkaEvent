namespace KartoshkaEvent.Domain.Models
{
    public record Location
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int NumberOfhouse { get; set; }
        public ModerationStatus ModerationStatus { get; set; }
        public string? Mood { get; set; }
        public bool IsRejected => ModerationStatus == ModerationStatus.Rejected;

        public TicketInfo TicketInfo { get; set; }
        public TimeOfEvent TimeOfEvent { get; set; }
        public InfoRejectedEvent? InfoRejectedEvent { get; set; }

        public List<Ticket> Tickets { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
