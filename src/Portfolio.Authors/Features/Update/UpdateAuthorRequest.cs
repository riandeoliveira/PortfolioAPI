using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Features.Update;

public sealed record UpdateAuthorRequest
(
    Guid Id,
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
) : IRequest<UpdateAuthorResponse>;
