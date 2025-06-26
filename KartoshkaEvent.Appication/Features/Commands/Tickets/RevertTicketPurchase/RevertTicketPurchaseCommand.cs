using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.RefundTicketWebhook;
using MediatR;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.RevertTicketPurchase
{
    public class RevertTicketPurchaseCommand : IRequest
    {
        public YooKassaRefundTicketNotificationDto NotificationDto { get; set; }
    }
}
