using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class NumberOfHouseChangeRule : IRequiresModerationEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            var newAddress = newEvent.LocationsOfEvents.FirstOrDefault()!;
            var oldAddress = oldEvent.LocationsOfEvents.FirstOrDefault()!;

            oldAddress.NumberOfhouse = newAddress.NumberOfhouse;
        }

        public bool RequiresModeration(Event oldEvent, Event newEvent)
        {
            var newAddress = newEvent.LocationsOfEvents.FirstOrDefault()!;
            var oldAddress = oldEvent.LocationsOfEvents.FirstOrDefault()!;

            return (newAddress.NumberOfhouse != oldAddress.NumberOfhouse)
                &&
                (newAddress.NumberOfhouse != 0);
        }
    }
}
