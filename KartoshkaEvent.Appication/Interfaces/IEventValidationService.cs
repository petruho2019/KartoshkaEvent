using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IEventValidationService
    {
        Result ExistEvent(Event @event);
        Result ValidateNewEventTime(TimeOfEvent oldTime, TimeOfEvent newTime);
        Result EnsureEditableByModerationStatus(ModerationStatus moderationStatus);
    }
}
