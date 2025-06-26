using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.FullRefund.Cache;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.RevertTicketPurchase
{
    public class RevertTicketPurchaseHandler(
        IKartoshkaEventContext context,
        ICacheService cacheService) : IRequestHandler<RevertTicketPurchaseCommand>
    {
        public async Task Handle(RevertTicketPurchaseCommand request, CancellationToken ct)
        {
            var refundInfoCache = await cacheService.GetAsync<FullRefundInfoCache>($"refund:{request.NotificationDto.Object.Id}", ct) 
                ?? throw new ArgumentNullException(message: "Объект информации о возврате токена из cache оказался null", new());

            var ticketByPaymentId = await context
                .Tickets
                .Include(t => t.EventLocation)
                    .ThenInclude(l => l.TicketInfo)
                .FirstOrDefaultAsync(t => t.PaymentId.Equals(request.NotificationDto.Object.PaymentId), ct) 
                    ?? throw new ArgumentNullException(message: "Неожиданный null при получении объекта билета по идентификатору оплаты", new());

            ticketByPaymentId.Status = Domain.Models.TicketStatus.Returned;
            ticketByPaymentId.EventLocation.TicketInfo.Quantity += refundInfoCache.Quantity;

            await context.SaveChangesAsync(ct);
        }
    }
}
