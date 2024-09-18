using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AspNetTemplate.Domain.Entities;

[Table("users")]
public sealed class User : BaseEntity
{
    [Column("email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [MaxLength(64)]
    [MinLength(8)]
    [NotNull]
    [Required]
    public required string Email { get; set; }

    [Column("password")]
    [DataType(DataType.Password)]
    [MaxLength(64)]
    [MinLength(8)]
    [NotNull]
    [Required]
    public required string Password { get; set; }
}
