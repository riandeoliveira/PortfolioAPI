
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Mapping;

public sealed class AuthorMap : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ConfigureAuditProperties("authors");

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
