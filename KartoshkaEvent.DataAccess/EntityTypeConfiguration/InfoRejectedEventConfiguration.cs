using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    public class InfoRejectedEventConfiguration(string tableName, string schema) : IEntityTypeConfiguration<InfoRejectedEvent>
    {
        public void Configure(EntityTypeBuilder<InfoRejectedEvent> builder)
        {
            builder.HasKey(e => e.Id).HasName("InfoRejectedEvent_pkey");
            builder.ToTable(tableName, schema);

            builder
                .HasOne(i => i.Address)
                .WithOne(a => a.InfoRejectedEvent)
                .HasForeignKey<InfoRejectedEvent>(i => i.AddressId);

        }
    }
}
