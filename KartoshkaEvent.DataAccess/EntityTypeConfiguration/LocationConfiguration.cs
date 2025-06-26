using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    public class LocationConfiguration(string tableName, string schema) : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(e => e.Id).HasName("EventAddress_pkey");
            builder.ToTable(tableName, schema);

            builder
                .HasOne(a => a.Event)
                .WithMany(e => e.LocationsOfEvents)
                .HasForeignKey(a => a.EventId);
        }
    }
}
