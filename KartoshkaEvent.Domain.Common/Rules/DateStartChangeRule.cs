using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class DateStartChangeRule : IRequiresModerationEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            var newTime = newEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;
            var oldTime = oldEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;

            oldTime.DateStart = newTime.DateStart.ToUniversalTime();
        }

        public bool RequiresModeration(Event oldEvent, Event newEvent)
        {
            var newTime = newEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;
            var oldTime = oldEvent.LocationsOfEvents.FirstOrDefault()!.TimeOfEvent;

            return
                (newTime.DateStart != oldTime.DateStart)
                &&
                (newTime.DateStart != DateTime.MinValue);
        }
    }
}
