using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PortfolioAPI.Entities;

public abstract class BaseEntity
{
    [Column("id")]
    [Key]
    [NotNull]
    [Required]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Column("created_at")]
    [DataType(DataType.DateTime)]
    [NotNull]
    [Required]
    public DateTime CreatedAt { get; private set; } = DateTime.Now;

    [Column("updated_at")]
    [DataType(DataType.DateTime)]
    public DateTime? UpdatedAt { get; private set; } = null;

    [Column("excluded_at")]
    [DataType(DataType.DateTime)]
    public DateTime? ExcludedAt { get; private set; } = null;
}
