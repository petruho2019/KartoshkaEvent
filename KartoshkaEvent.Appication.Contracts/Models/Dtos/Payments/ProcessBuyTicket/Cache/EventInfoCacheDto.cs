namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache
{
    public class EventInfoCacheDto
    {
        public Guid EventId { get; set; }
        public string Subject { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
