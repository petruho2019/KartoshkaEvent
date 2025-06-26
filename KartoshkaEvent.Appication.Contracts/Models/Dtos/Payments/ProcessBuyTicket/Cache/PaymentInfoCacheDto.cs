namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache
{
    public class PaymentInfoCacheDto
    {
        public Guid PaymentId { get; set; }
        public string BuyerEmail { get; set; }
        public string OwnerEmail { get; set; }
        public SoldTicketCacheDto TicketInfo { get; set; }
        public EventInfoCacheDto EventInfo { get; set; }
        public LocationInfoCacheDto LocationInfo { get; set; }
    }
}
