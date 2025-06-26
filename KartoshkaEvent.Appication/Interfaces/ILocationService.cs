using KartoshkaEvent.Application.Features.Commands.Events.AddForModeration;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface ILocationService
    {
        List<Location> BuildLocationWithTicketInfoAndTimeOfEvent(Guid eventId, AddEventForModerationCommand command);
    }
}
