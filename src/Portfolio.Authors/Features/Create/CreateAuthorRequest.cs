using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Features.Create;

public sealed record CreateAuthorRequest(
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
) : IRequest<CreateAuthorResponse>;
