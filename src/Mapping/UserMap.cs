using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioAPI.Extensions;

namespace PortfolioAPI.Mapping;

public sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureAuditProperties("users");

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(u => u.Position)
            .HasColumnName("position")
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(u => u.Description)
            .HasColumnName("description")
            .HasMaxLength(1000)
            .IsRequired();

        builder
            .Property(u => u.AvatarUrl)
            .HasColumnName("avatar_url")
            .HasMaxLength(500)
            .IsRequired();

        builder
            .Property(u => u.SpotifyAccountName)
            .HasColumnName("spotify_account_name")
            .HasMaxLength(50);
    }
}
