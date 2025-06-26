using KartoshkaEvent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KartoshkaEvent.DataAccess.EntityTypeConfiguration
{
    class UserConfiguration(string tableName, string schema) : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id).HasName("User_pkey");
            builder.ToTable(tableName, schema);

            builder.Property(e => e.Password)
                .IsRequired();
            builder.Property(e => e.Email)
                .IsRequired();
        }
    }
}

