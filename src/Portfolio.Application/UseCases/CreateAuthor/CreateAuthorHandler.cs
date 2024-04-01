using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.CreateAuthor;

public sealed class CreateAuthorHandler(
    IAuthorRepository authorRepository,
    IAuthService authService,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateAuthorRequest, CreateAuthorResponse>
{
    public async Task<CreateAuthorResponse> Handle(CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        Author author = new()
        {
            Name = request.Name,
            FullName = request.FullName,
            Position = request.Position,
            Description = request.Description,
            AvatarUrl = request.AvatarUrl,
            SpotifyAccountName = request.SpotifyAccountName,
            UserId = authService.GetLoggedInUserId()
        };

        Author createdAuthor = await authorRepository.CreateAsync(author, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return new CreateAuthorResponse(createdAuthor.Adapt<AuthorDto>());
    }
}
