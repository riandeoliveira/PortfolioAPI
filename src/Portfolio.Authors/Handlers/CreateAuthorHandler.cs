using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Authors.Validators;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class CreateAuthorHandler
(
    CreateAuthorValidator validator,
    IAuthorRepository authorRepository,
    IAuthService authService
) : IRequestHandler<CreateAuthorRequest, Author>
{
    public async Task<Author> Handle(CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateRequestAsync(request, cancellationToken);

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

        return createdAuthor;
    }
}
