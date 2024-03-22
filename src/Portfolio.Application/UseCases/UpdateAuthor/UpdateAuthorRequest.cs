using MediatR;

namespace Portfolio.Application.UseCases.UpdateAuthor;

public sealed record UpdateAuthorRequest(
    Guid Id,
    string Name,
    string FullName,
    string Position,
    string Description,
    string AvatarUrl,
    string? SpotifyAccountName
) : IRequest<UpdateAuthorResponse>;
