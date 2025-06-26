using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class DateEndChangeRule : IRequiresModerationEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            var newTime = newEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;
            var oldTime = oldEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;

            oldTime.DateEnd = newTime.DateEnd.ToUniversalTime();
        }

        public bool RequiresModeration(Event oldEvent, Event newEvent)
        {
            var newTime = newEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;
            var oldTime = oldEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;

            return
                (newTime.DateEnd != oldTime.DateEnd)
                &&
                (newTime.DateEnd != DateTime.MinValue);
                
        }
    }
}
