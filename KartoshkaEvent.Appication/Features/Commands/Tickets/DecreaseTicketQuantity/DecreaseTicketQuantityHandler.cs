using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.DecreaseTicketQuantity
{
    public class DecreaseTicketQuantityHandler(
        ICacheService cacheService,
        IKartoshkaEventContext context) : IRequestHandler<DecreaseTicketQuantityCommand>
    {
        public async Task Handle(DecreaseTicketQuantityCommand request, CancellationToken ct)
        {
            var paymentInfoFromCache = await cacheService.GetAsync<PaymentInfoCacheDto>($"payment:{request.PaymentNotification.Object.Id}", ct);

            var @event =  await context
                .Events
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TicketInfo)
                .Where(e => e.Id.Equals(paymentInfoFromCache!.EventInfo.EventId) && e.LocationsOfEvents.Any(l => l.Id.Equals(paymentInfoFromCache.LocationInfo.LocationId)))
                .FirstOrDefaultAsync(ct);

            var location = @event!.LocationsOfEvents.Where(l => l.Id == paymentInfoFromCache!.LocationInfo.LocationId).First();

            location.TicketInfo.Quantity -= paymentInfoFromCache!.TicketInfo.QuantityOfTickets;

            await context.SaveChangesAsync(ct);

        }
    }
}
