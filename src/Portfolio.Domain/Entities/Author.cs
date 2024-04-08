using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Domain.Entities;

[Table("authors")]
public sealed class Author : BaseEntity
{
    [Column("name")]
    [DataType(DataType.Text)]
    [MaxLength(64)]
    [NotNull]
    [Required]
    public required string Name { get; set; }

    [Column("full_name")]
    [DataType(DataType.Text)]
    [MaxLength(128)]
    [NotNull]
    [Required]
    public required string FullName { get; set; }

    [Column("position")]
    [DataType(DataType.Text)]
    [MaxLength(64)]
    [NotNull]
    [Required]
    public required string Position { get; set; }

    [Column("description")]
    [DataType(DataType.MultilineText)]
    [MaxLength(1024)]
    [NotNull]
    [Required]
    public required string Description { get; set; }

    [Column("avatar_url")]
    [DataType(DataType.ImageUrl)]
    [MaxLength(512)]
    [NotNull]
    [Required]
    public required string AvatarUrl { get; set; }

    [Column("spotify_account_name")]
    [DataType(DataType.Text)]
    [MaxLength(64)]
    public string? SpotifyAccountName { get; set; }

    [Column("user_id")]
    [ForeignKey("User")]
    [NotNull]
    [Required]
    public required Guid UserId { get; set; }

    public User? User { get; set; }
}
