using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    public class EventConfiguration(string tableName, string schema) : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id).HasName("Event_pkey");
            builder.ToTable(tableName, schema);

            builder
                .HasOne(e => e.Owner)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.OwnerId);
        }
    }
}
