using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Extensions;

internal static class EntityMappingExtension
{
    internal static void ConfigureAuditProperties<TEntity>(this EntityTypeBuilder<TEntity> builder, string table) where TEntity : BaseEntity
    {
        builder
            .ToTable(table)
            .HasKey(x => x.Id);

        builder
            .Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder
            .Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");

        builder
            .Property(e => e.ExcludedAt)
            .HasColumnName("excluded_at");
    }
}
