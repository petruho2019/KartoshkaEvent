using KartoshkaEvent.Application.Contracts.Interfaces;
using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IKartoshkaEventContext : IDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<TimeOfEvent> EventTimes { get; set; }
        DbSet<Location> EventLocations { get; set; }
        DbSet<EventImage> EventImages { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<TicketInfo> TicketsInfo { get; set; }
        DbSet<Ticket> Tickets { get; set; }

        DbSet<InfoRejectedEvent> InfoRejectedEvents { get; set; }
    }
}
