using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Infrastructure.Extensions;

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
            .Property(e => e.DeletedAt)
            .HasColumnName("deleted_at");
    }
}
