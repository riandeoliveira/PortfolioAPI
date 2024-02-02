namespace Portfolio.Authors.Requests;

public record CreateAuthorRequest
(
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
);
