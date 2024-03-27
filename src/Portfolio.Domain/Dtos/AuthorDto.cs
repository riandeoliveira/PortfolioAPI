namespace Portfolio.Domain.Dtos;

public sealed record AuthorDto(
    Guid Id,
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
);
