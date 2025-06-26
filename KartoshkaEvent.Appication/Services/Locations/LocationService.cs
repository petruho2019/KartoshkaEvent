using KartoshkaEvent.Application.Features.Commands.Events.AddForModeration;
using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Services.Locations
{
    public class LocationService : ILocationService
    {
        public List<Location> BuildLocationWithTicketInfoAndTimeOfEvent(Guid eventId, AddEventForModerationCommand command)
        {
            var addresses = new List<Location>();

            foreach (var addressDto in command.Locations)
            {
                var address = new Location()
                {
                    Id = Guid.NewGuid(),
                    City = addressDto.City,
                    Street = addressDto.Street,
                    NumberOfhouse = addressDto.NumberOfHouse,
                    ModerationStatus = ModerationStatus.OnModeration,
                    EventId = eventId

                };
                address.TicketInfo = new()
                {
                    Id = address.Id,
                    Price = addressDto.PriceOfTicket,
                    Quantity = addressDto.QuantityOfTickets
                };
                address.TimeOfEvent = new()
                {
                    Id = address.Id,
                    DateStart = addressDto.DateStart.ToUniversalTime(),
                    DateEnd = addressDto.DateEnd.ToUniversalTime()
                }; ;

                addresses.Add(address);
            }

            return addresses;
        }
    }
}
