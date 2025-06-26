using KartoshkaEvent.Application.Contracts.Models.Dtos.Qr.Tickets.GetInfoByQr;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Qr.GetInfoByQr
{
    public class QrInfoResponse
    {
        public Guid TicketId { get; set; }

        public QrBuyerInfo BuyerInfo { get; set; }
        public QrTimeInfo TimeInfo { get; set; }
        public QrLocationInfo LocationInfo { get; set; }
        public QrEventInfo EventInfo { get; set; }
    }
}
