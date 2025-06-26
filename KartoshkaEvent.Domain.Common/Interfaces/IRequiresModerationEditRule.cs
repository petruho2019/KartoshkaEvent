using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Interfaces
{
    public interface IRequiresModerationEditRule : IEditRule
    {
        bool RequiresModeration(Event oldEvent, Event newEvent);

    }
}
