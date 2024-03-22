using MediatR;

namespace Portfolio.Application.UseCases.CreateAuthor;

public sealed record CreateAuthorRequest(
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
) : IRequest<CreateAuthorResponse>;
