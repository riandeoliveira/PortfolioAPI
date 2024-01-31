using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PortfolioAPI.Entities;

namespace PortfolioAPI.Extensions;

public static class EntityMappingExtension
{
    public static void ConfigureAuditProperties<T>(this EntityTypeBuilder<T> builder, string table) where T : BaseEntity
    {
        builder
            .ToTable(table)
            .HasKey(x => x.Id);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.UpdatedAt)
               .HasColumnName("updated_at");

        builder.Property(e => e.ExcludedAt)
               .HasColumnName("excluded_at");
    }
}
