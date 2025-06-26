using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.DataAccess.EntityTypeConfiguration;
using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KartoshkaEvent.DataAccess
{
    public class KartoshkaEventContext : DbContext, IKartoshkaEventContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<TimeOfEvent> EventTimes { get; set; }
        public DbSet<Location> EventLocations { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TicketInfo> TicketsInfo { get; set; }
        public DbSet<InfoRejectedEvent> InfoRejectedEvents { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        public KartoshkaEventContext() { }

        public KartoshkaEventContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration("User", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration("RefreshToken", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new LocationConfiguration("EventAddress", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new EventTimeConfiguration("EventTime", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new EventImageConfiguration("EventImage", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new EventConfiguration("Event", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new TagConfiguration("Tag", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new InfoRejectedEventConfiguration("InfoRejectedEvent", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new TicketInfoConfiguration("TicketInfo", "KartoshkaEvent"));
            modelBuilder.ApplyConfiguration(new TicketConfiguration("Ticket", "KartoshkaEvent"));
        }
    }
}
