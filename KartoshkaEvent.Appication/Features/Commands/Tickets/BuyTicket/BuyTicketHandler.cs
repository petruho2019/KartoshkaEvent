using AutoMapper;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Payments.ProcessBuyTicket.Cache;
using KartoshkaEvent.Application.Contracts.Models.Dtos.Ticket.Buy;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Application.Services;
using KartoshkaEvent.Applicationю.Contracts.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace KartoshkaEvent.Application.Features.Commands.Tickets.BuyTicket
{
    public class BuyTicketHandler(
        IKartoshkaEventContext context,
        IEventValidationService eventValidationService,
        IYooKassaService yooKassaService,
        CurrentUserService currentUserService,
        IMapper mapper,
        ICacheService cacheService) : IRequestHandler<BuyTicketCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(BuyTicketCommand request, CancellationToken ct)
        {
            var @event = await context
                .Events
                .Include(e => e.Owner)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TicketInfo)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.Tickets)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TimeOfEvent)
                .Where(e => e.Id.Equals(request.EventId) && e.LocationsOfEvents.Any(l => l.Id.Equals(request.AddressId)))
                .FirstOrDefaultAsync(ct);

            var validationResult = eventValidationService.ExistEvent(@event);
            if (!validationResult.IsSuccess)
                return Result<string>.FromError(validationResult.Error!);

            var location = @event.LocationsOfEvents.Where(l => l.Id == request.AddressId).First();

            if (location.ModerationStatus != Domain.Models.ModerationStatus.Approved)
                return Result<string>.BadRequest("Мероприятие не найдено");

            var quantityFreeTickets = location.TicketInfo.Quantity - location.Tickets.Sum(t => t.TotalQuantity);
            if (quantityFreeTickets < request.Quantity)
                return Result<string>.BadRequest($"Недостаточно билетов, запрошено {request.Quantity} достпуно {quantityFreeTickets}");


            var paymentObject = mapper.Map<TicketPaymentObject>(location);
            paymentObject.QuantityOfTickets = request.Quantity;
            paymentObject.BuyerEmail = currentUserService.Email;
            paymentObject.SubjectOfEvent = @event.Subject;

            var paymentResult = await yooKassaService.BuyTicket(paymentObject);

            if (paymentResult.Status != null && paymentResult.Status.Equals("pending", StringComparison.OrdinalIgnoreCase))
            {
                var paymentInfo = mapper.Map<PaymentInfoCacheDto>(paymentObject);

                paymentInfo.EventInfo.DateStart = location.TimeOfEvent.DateStart;
                paymentInfo.EventInfo.DateEnd = location.TimeOfEvent.DateEnd;

                paymentInfo.EventInfo.EventId = @event.Id;
                paymentInfo.LocationInfo.LocationId = location.Id;

                paymentInfo.OwnerEmail = @event.Owner.Email;

                paymentInfo.PaymentId = Guid.Parse(paymentResult.PaymentId);

                await cacheService.SetAsync($"payment:{paymentResult.PaymentId}", paymentInfo, TimeSpan.FromHours(3), ct);

                return Result<string>.Ok(paymentResult.PaymentUrl);
            }


            return Result<string>.BadRequest(paymentResult.Description);
        }
    }
}
