using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AspNetTemplate.Domain.Entities;

[Table("personal_refresh_tokens")]
public sealed class PersonalRefreshToken : BaseEntity
{
    [Column("value")]
    [NotNull]
    [Required]
    [StringLength(329)]
    public required string Value { get; set; }

    [Column("expires_in")]
    [DataType(DataType.DateTime)]
    [NotNull]
    [Required]
    public required DateTime ExpiresIn { get; set; }

    [Column("has_been_used")]
    [DefaultValue(false)]
    [NotNull]
    [Required]
    public bool HasBeenUsed { get; set; } = false;

    [Column("user_id")]
    [ForeignKey("User")]
    [NotNull]
    [Required]
    public required Guid UserId { get; set; }

    public User? User { get; set; } = null;
}
