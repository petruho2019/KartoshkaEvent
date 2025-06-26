using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    public class TicketConfiguration(string tableName, string schema) : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.Id).HasName("Ticket_pkey");
            builder.ToTable(tableName, schema);

            builder
                .HasOne(t => t.Buyer)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.BuyerId);

            builder
                .HasOne(t => t.EventLocation)
                .WithMany(el => el.Tickets)
                .HasForeignKey(t => t.EventLocationId);
        }
    }
}
