using Portfolio.Authors.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Features.Create;

public sealed class CreateAuthorHandler(
    CreateAuthorValidator validator,
    IAuthorRepository authorRepository,
    IAuthService authService
) : IRequestHandler<CreateAuthorRequest, CreateAuthorResponse>
{
    public async Task<CreateAuthorResponse> Handle(CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        Author author = new()
        {
            Name = request.Name.Trim(),
            FullName = request.FullName.Trim(),
            Position = request.Position.Trim(),
            Description = request.Description.Trim(),
            AvatarUrl = request.AvatarUrl.Trim(),
            SpotifyAccountName = request.SpotifyAccountName?.Trim(),
            UserId = authService.GetLoggedInUserId()
        };

        Author createdAuthor = await authorRepository.CreateAsync(author, cancellationToken);

        await authorRepository.SaveChangesAsync(cancellationToken);

        return new CreateAuthorResponse(createdAuthor);
    }
}
