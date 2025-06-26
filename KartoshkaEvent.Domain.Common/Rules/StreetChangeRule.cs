using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class StreetChangeRule : IRequiresModerationEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            var newAddress = newEvent.LocationsOfEvents.FirstOrDefault()!;
            var oldAddress = oldEvent.LocationsOfEvents.FirstOrDefault()!;

            oldAddress.Street = newAddress.Street;
        }

        public bool RequiresModeration(Event oldEvent, Event newEvent)
        {
            var newAddress = newEvent.LocationsOfEvents.FirstOrDefault()!;
            var oldAddress = oldEvent.LocationsOfEvents.FirstOrDefault()!;

            return (newAddress.Street != oldAddress.Street)
                &&
                (!string.IsNullOrEmpty(newAddress.Street));
        }
    }
}
