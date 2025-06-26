using KartoshkaEvent.Application.Common.Mappings;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.FullRefund;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.BuyTicketWebhook;

namespace KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund.Cache
{
    public class FullRefundInfoCache : IMapWith<FullRefundDto>
    {
        public Guid PaymentId { get; set; }
        public long Quantity { get; set; } = default!;
        public string BuyerEmail { get; set; }
    }
}
