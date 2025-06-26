using AutoMapper;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Common.Rules;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Features.Commands.Events.Edit
{
    public class EditEventHandler(
        IKartoshkaEventContext context,
        IEventValidationService eventValidationService,
        IMapper mapper) : IRequestHandler<EditEventCommand, Result>
    {
        public async Task<Result> Handle(EditEventCommand request, CancellationToken ct)
        {
            var @event = await context
                .Events
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TimeOfEvent)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.TicketInfo)
                .Include(e => e.LocationsOfEvents)
                    .ThenInclude(l => l.Tickets)
                .Where(e => e.Id.Equals(request.EventId) && e.LocationsOfEvents.Any(l => l.Id == request.AddressId))
                .FirstOrDefaultAsync(ct);

            var validationExistResult = eventValidationService.ExistEvent(@event!);
            if (!validationExistResult.IsSuccess)
                return Result.FromError(validationExistResult.Error!);

            var location = @event!.LocationsOfEvents.Where(l => l.Id == request.AddressId).First();

            var validationNewEventTimeResult = eventValidationService.ValidateNewEventTime(location.TimeOfEvent, new() { DateEnd = request.DateEnd, DateStart = request.DateStart });
            if (!validationNewEventTimeResult.IsSuccess)
                return Result.FromError(validationNewEventTimeResult.Error!);

            var ensureEditableByModerationStatusResult = eventValidationService.EnsureEditableByModerationStatus(location.ModerationStatus);
            if (!ensureEditableByModerationStatusResult.IsSuccess)
                return Result.FromError(ensureEditableByModerationStatusResult.Error!);

            if (request.QuantityOfTickets != 0
                &&
                location.Tickets.Sum(t => t.TotalQuantity) > request.QuantityOfTickets)
                return Result.BadRequest("Новое количество билет должно быть больше уже купленного количества");
            if (request.PriceOfTickets != 0
                &&
                location.TicketInfo.Quantity == location.Tickets.Sum(t => t.TotalQuantity))
                return Result.BadRequest("Вы не можете изменить цену билетов так как все билеты раскупили");

            var rules = new List<IEditRule>
            {
                new DateStartChangeRule(),
                new DateEndChangeRule(),
                new CityChangeRule(),
                new NumberOfHouseChangeRule(),
                new StreetChangeRule(),
                new EventTypeChangeRule(),
                new SubjectChangeRule(),
            };

            if (!string.IsNullOrEmpty(request.Description))
                rules.Add(new DescriptionChangeRule());
            if (request.QuantityOfTickets != 0)
                rules.Add(new QuantityOfTicketsChangeRule());
            if (request.PriceOfTickets != 0)
                rules.Add(new PriceOfTicketsChangeRule());

            var newEvent = BuildEventFromCommand(request, mapper);

            foreach (var rule in rules)
            {
                if (rule is IRequiresModerationEditRule r && r.RequiresModeration(@event, newEvent))
                {
                    rule.ApplyChange(ref @event, newEvent);
                    location.ModerationStatus = ModerationStatus.OnModerationEdit;
                }
                if (rule is not IRequiresModerationEditRule)
                {
                    rule.ApplyChange(ref @event, newEvent);
                }
            }

            await context.SaveChangesAsync(ct);

            return Result.NoContent();
        }

        public static Event BuildEventFromCommand(EditEventCommand command, IMapper mapper)
        {
            var @event = mapper.Map<Event>(command);

            var location = mapper.Map<Location>(command);
            var time = mapper.Map<TimeOfEvent>(command);
            var ticket = mapper.Map<TicketInfo>(command);

            location.TimeOfEvent = time;
            location.TicketInfo = ticket;

            @event.LocationsOfEvents = [location];

            return @event;
        }

    }
}
