
using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class EventTypeChangeRule : IRequiresModerationEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            oldEvent.EventType = newEvent.EventType;
        }

        public bool RequiresModeration(Event oldEvent, Event newEvent)
        {
            return !newEvent.EventType.Equals(EventType.Unknown) && !newEvent.EventType.Equals(oldEvent.EventType);
        }
    }
}
