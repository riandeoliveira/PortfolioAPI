using Portfolio.Entities;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Requests;

public sealed record CreateAuthorRequest
(
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
) : IRequest<Author>;
