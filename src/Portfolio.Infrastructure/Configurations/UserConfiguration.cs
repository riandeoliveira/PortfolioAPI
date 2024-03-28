using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureAuditProperties("users");

        builder
            .Property(x => x.Email)
            .HasColumnName("email")
            .HasMaxLength(64)
            .IsRequired();

        builder
            .Property(x => x.Password)
            .HasColumnName("password")
            .HasMaxLength(64)
            .IsRequired();
    }
}
