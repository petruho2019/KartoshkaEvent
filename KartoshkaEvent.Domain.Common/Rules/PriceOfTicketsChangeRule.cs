using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class PriceOfTicketsChangeRule : IEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            oldEvent.LocationsOfEvents.First().TicketInfo.Price = newEvent.LocationsOfEvents.First().TicketInfo.Price;
        }
    }
}
