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
    private readonly CreateAuthorValidator _validator = validator;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IAuthService _authService = authService;

    public async Task<Author> Handle(CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateRequestAsync(request, cancellationToken);

        Author author = new()
        {
            Name = request.Name.Trim(),
            FullName = request.FullName.Trim(),
            Position = request.Position.Trim(),
            Description = request.Description.Trim(),
            AvatarUrl = request.AvatarUrl.Trim(),
            SpotifyAccountName = request.SpotifyAccountName?.Trim(),
            UserId = _authService.GetLoggedInUserId()
        };

        Author createdAuthor = await _authorRepository.CreateAsync(author, cancellationToken);

        await _authorRepository.SaveChangesAsync(cancellationToken);

        return createdAuthor;
    }
}
