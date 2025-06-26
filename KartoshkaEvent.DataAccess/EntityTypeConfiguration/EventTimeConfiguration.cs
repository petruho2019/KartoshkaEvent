using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    public class EventTimeConfiguration(string tableName, string schema) : IEntityTypeConfiguration<TimeOfEvent>
    {
        public void Configure(EntityTypeBuilder<TimeOfEvent> builder)
        {
            builder.HasKey(rt => rt.Id).HasName("EventTime_pkey");
            builder.ToTable(tableName, schema);

            builder
                .HasOne(t => t.Location)
                .WithOne(a => a.TimeOfEvent)
                .HasForeignKey<TimeOfEvent>(t => t.LocationId);
        }
    }
}
