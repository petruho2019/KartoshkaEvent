using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    public class TicketInfoConfiguration(string tableName, string schema) : IEntityTypeConfiguration<TicketInfo>
    {
        public void Configure(EntityTypeBuilder<TicketInfo> builder)
        {
            builder.HasKey(e => e.Id).HasName("TicketInfo_pkey");
            builder.ToTable(tableName, schema);

            builder
                .HasOne(a => a.Address)
                .WithOne(a => a.TicketInfo)
                .HasForeignKey<Location>(a => a.Id);
        }
    }
}
