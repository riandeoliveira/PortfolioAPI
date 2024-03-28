using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.CreateAuthor;

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

        return new CreateAuthorResponse(createdAuthor.Adapt<AuthorDto>());
    }
}
