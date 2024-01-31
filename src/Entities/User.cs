using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PortfolioAPI.Entities;

[Table("users")]
public sealed class User(
    string name,
    string fullName,
    string position,
    string description,
    string avatarUrl,
    string? spotifyAccountName = null
) : BaseEntity
{
    [Column("name")]
    [DataType(DataType.Text)]
    [NotNull]
    [Required]
    public string Name { get; private set; } = name;

    [Column("full_name")]
    [DataType(DataType.Text)]
    [NotNull]
    [Required]
    public string FullName { get; private set; } = fullName;

    [Column("position")]
    [DataType(DataType.Text)]
    [NotNull]
    [Required]
    public string Position { get; private set; } = position;

    [Column("description")]
    [DataType(DataType.MultilineText)]
    [NotNull]
    [Required]
    public string Description { get; private set; } = description;

    [Column("avatar_url")]
    [DataType(DataType.ImageUrl)]
    [NotNull]
    [Required]
    public string AvatarUrl { get; private set; } = avatarUrl;

    [Column("spotify_account_name")]
    [DataType(DataType.Text)]
    [Key]
    public string? SpotifyAccountName { get; private set; } = spotifyAccountName;
}
