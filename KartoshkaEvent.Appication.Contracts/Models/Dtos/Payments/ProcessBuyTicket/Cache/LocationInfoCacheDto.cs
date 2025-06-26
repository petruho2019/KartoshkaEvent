namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache
{
    public class LocationInfoCacheDto
    {
        public Guid LocationId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int NumberOfHouse { get; set; }
    }
}
