using AspNetTemplate.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetTemplate.Infra.Common.Extensions;

internal static class EntityMappingExtension
{
    internal static void ConfigureAuditProperties<TEntity>(this EntityTypeBuilder<TEntity> builder, string table) where TEntity : BaseEntity
    {
        builder
            .ToTable(table)
            .HasKey(x => x.Id);

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

        builder
            .Property(x => x.DeletedAt)
            .HasColumnName("deleted_at");
    }
}
