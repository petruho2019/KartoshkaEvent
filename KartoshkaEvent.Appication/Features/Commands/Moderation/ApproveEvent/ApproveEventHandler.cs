using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Moderation.ApproveEvent
{
    public class ApproveEventHandler(
        IKartoshkaEventContext context,
        IEventValidationService eventValidationService) : IRequestHandler<ApproveEventCommand, Result>
    {
        public async Task<Result> Handle(ApproveEventCommand request, CancellationToken ct)
        {
            var @event = await context
                .Events
                .Include(e => e.LocationsOfEvents)
                .Where(e => e.Id.Equals(request.EventId))
                .Select(e => new Event()
                {
                    LocationsOfEvents = e.LocationsOfEvents.Where(a => a.Id.Equals(request.AddressId)).ToList()
                })
                .FirstOrDefaultAsync(ct);

            var validationResult = eventValidationService.ExistEvent(@event);
            if (!validationResult.IsSuccess)
                return Result.FromError(validationResult!.Error!);

            var address = @event.LocationsOfEvents.FirstOrDefault(afe => afe.Id.Equals(request.AddressId))!;
            if (!address.ModerationStatus.Equals(ModerationStatus.OnModeration) && !address.ModerationStatus.Equals(ModerationStatus.OnModerationEdit))
                return Result.BadRequest("Мероприятие не на модерации");

            address.ModerationStatus = ModerationStatus.Approved;

            await context.SaveChangesAsync(ct);

            return Result.NoContent();
        }
    }
}
