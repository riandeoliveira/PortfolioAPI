using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Domain.Entities;

public abstract class BaseEntity
{
    [Column("id")]
    [Key]
    [NotNull]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("created_at")]
    [DataType(DataType.DateTime)]
    [NotNull]
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    [DataType(DataType.DateTime)]
    public DateTime? UpdatedAt { get; set; } = null;

    [Column("removed_at")]
    [DataType(DataType.DateTime)]
    public DateTime? RemovedAt { get; set; } = null;
}
