using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KartoshkaEvent.Application.Features.Commands.Moderation.RejectEvent
{
    public class RejectEventHandler(
        IKartoshkaEventContext context,
        IConfiguration configuration,
        IEventValidationService eventValidationService) : IRequestHandler<RejectEventCommand, Result>
    {
        public async Task<Result> Handle(RejectEventCommand request, CancellationToken ct)
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
                return Result.FromError(validationResult.Error);

            var address = @event.LocationsOfEvents.FirstOrDefault(afe => afe.Id.Equals(request.AddressId))!;
            if (!address.ModerationStatus.Equals(ModerationStatus.OnModeration))
                return Result.BadRequest("Статус мероприятие не 'На модерации'");

            address.ModerationStatus = ModerationStatus.Rejected;

            var infoRejected = new InfoRejectedEvent()
            {
                Id = Guid.NewGuid(),
                Address = address,
                Email = configuration["Admin:Email"]!,
                Message = request.RejectedMessage
            };

            await context.InfoRejectedEvents.AddAsync(infoRejected);
            await context.SaveChangesAsync(ct);

            return Result.NoContent();
        }
    }
}
