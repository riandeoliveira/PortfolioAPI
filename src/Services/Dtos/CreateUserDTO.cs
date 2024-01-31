namespace PortfolioAPI.Services.Dtos;

public record CreateUserDTO
(
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
);
