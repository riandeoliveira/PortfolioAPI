using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Entities;

[Table("users")]
public sealed class User : BaseEntity
{
    [Column("email")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [MaxLength(250)]
    [NotNull]
    [Required]
    public required string Email { get; set; }

    [Column("password")]
    [DataType(DataType.Password)]
    [MaxLength(250)]
    [NotNull]
    [Required]
    public required string Password { get; set; }
}
