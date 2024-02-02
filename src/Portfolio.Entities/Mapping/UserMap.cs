using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Portfolio.Entities.Extensions;

namespace Portfolio.Entities.Mapping;

public sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureAuditProperties("users");

        builder
            .Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(x => x.Password)
            .HasColumnName("password")
            .HasMaxLength(250)
            .IsRequired();
    }
}
