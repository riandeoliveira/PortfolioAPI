using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Portfolio.Entities;

[Table("authors")]
public sealed class Author : BaseEntity
{
    [Column("name")]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    [NotNull]
    [Required]
    public required string Name { get; set; }

    [Column("full_name")]
    [DataType(DataType.Text)]
    [MaxLength(100)]
    [NotNull]
    [Required]
    public required string FullName { get; set; }

    [Column("position")]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    [NotNull]
    [Required]
    public required string Position { get; set; }

    [Column("description")]
    [DataType(DataType.MultilineText)]
    [MaxLength(1000)]
    [NotNull]
    [Required]
    public required string Description { get; set; }

    [Column("avatar_url")]
    [DataType(DataType.ImageUrl)]
    [MaxLength(500)]
    [NotNull]
    [Required]
    public required string AvatarUrl { get; set; }

    [Column("spotify_account_name")]
    [DataType(DataType.Text)]
    [MaxLength(50)]
    public string? SpotifyAccountName { get; set; }

    /*[Column("user_id")]
    [ForeignKey("User")]
    [Key]
    [NotNull]
    [Required]
    public required Guid UserId { get; set; }

    public User? User { get; set; }*/
}
