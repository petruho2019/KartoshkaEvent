using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.AddSoldTicketToHistory
{
    public class AddSoldTicketToHistoryHandler(
        ICacheService cacheService,
        IKartoshkaEventContext context) : IRequestHandler<AddSoldTicketToHistoryCommand>
    {
        public async Task Handle(AddSoldTicketToHistoryCommand request, CancellationToken ct)
        {
            var paymentInfoFromCache = await cacheService.GetAsync<PaymentInfoCacheDto>($"payment:{request.PaymentNotification.Object.Id}", ct);

            var user = await context
                .Users
                .FirstOrDefaultAsync(u => u.Email.Equals(paymentInfoFromCache!.BuyerEmail));

            var ticket = new Ticket()
            {
                Id = Guid.NewGuid(),
                BuyerId = user!.Id,
                EventId = paymentInfoFromCache!.EventInfo.EventId,
                EventLocationId = paymentInfoFromCache.LocationInfo!.LocationId,
                PriceByTicket = paymentInfoFromCache.TicketInfo.PriceOfTickets / paymentInfoFromCache.TicketInfo.QuantityOfTickets,
                TotalQuantity = paymentInfoFromCache.TicketInfo.QuantityOfTickets,
                Status = TicketStatus.Active,
                PaymentId = paymentInfoFromCache.PaymentId
            };

            await context.Tickets.AddAsync(ticket);
            await context.SaveChangesAsync(ct);
        }
    }
}
