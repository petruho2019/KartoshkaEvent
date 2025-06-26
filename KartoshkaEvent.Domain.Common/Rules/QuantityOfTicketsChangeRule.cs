using KartoshkaEvent.Domain.Common.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Domain.Common.Rules
{
    public class QuantityOfTicketsChangeRule : IEditRule
    {
        public void ApplyChange(ref Event oldEvent, Event newEvent)
        {
            var newTicketInfo = newEvent.LocationsOfEvents.FirstOrDefault()!.TicketInfo;
            var oldTicketInfo = oldEvent.LocationsOfEvents.FirstOrDefault()!.TicketInfo;

            oldTicketInfo.Quantity = newTicketInfo.Quantity;
        }
    }
}
