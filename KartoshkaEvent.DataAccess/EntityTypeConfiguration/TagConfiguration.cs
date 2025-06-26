using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    public class TagConfiguration(string tableName, string schema) : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(e => e.Id).HasName("Tag_pkey");
            builder.ToTable(tableName, schema);

            builder
                .HasMany(a => a.Events)
                .WithMany(e => e.Tags)
                .UsingEntity("EventTag");
        }
    }
}
