using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class CityChangeRule : IRequiresModerationEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            var newAddress = newEvent.LocationsOfEvents.FirstOrDefault()!;
            var oldAddress = oldEvent.LocationsOfEvents.FirstOrDefault()!;

            oldAddress.City = newAddress.City;
        }

        public bool RequiresModeration(Event oldEvent, Event newEvent)
        {
            var newAddress = newEvent.LocationsOfEvents.FirstOrDefault()!;
            var oldAddress = oldEvent.LocationsOfEvents.FirstOrDefault()!;


            return (newAddress.City != oldAddress.City)
                &&
                (!string.IsNullOrEmpty(newAddress.City));
        }
    }
}
