using FluentValidation;
using FluentValidation.Results;

using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Authors.Validators;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class CreateAuthorHandler
(
    IAuthorRepository authorRepository,
    IAuthService authService,
    ILocalizationService localizationService
) : IRequestHandler<CreateAuthorRequest, Author>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IAuthService _authService = authService;
    private readonly ILocalizationService _localizationService = localizationService;

    public async Task<Author> Handle(CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        CreateAuthorValidator validator = new(_localizationService);
        ValidationResult result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }

        Author author = new()
        {
            Name = request.Name,
            FullName = request.FullName,
            Position = request.Position,
            Description = request.Description,
            AvatarUrl = request.AvatarUrl,
            SpotifyAccountName = request.SpotifyAccountName,
            UserId = _authService.GetLoggedInUserId()
        };

        Author createdAuthor = await _authorRepository.CreateAsync(author, cancellationToken);

        await _authorRepository.SaveChangesAsync(cancellationToken);

        return createdAuthor;
    }
}
