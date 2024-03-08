using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Authors.Validators;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Authors.Handlers;

public sealed class RemoveAuthorHandler
(
    IAuthorRepository authorRepository,
    ILocalizationService localizationService
) : IRequestHandler<RemoveAuthorRequest>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly ILocalizationService _localizationService = localizationService;

    public async Task Handle(RemoveAuthorRequest request, CancellationToken cancellationToken = default)
    {
        RemoveAuthorValidator validator = new(_authorRepository, _localizationService);
        ValidationResult result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }

        Author? author = await _authorRepository.FindAsync(request.Id, cancellationToken);

        await _authorRepository.Remove(author, cancellationToken);
        await _authorRepository.SaveChangesAsync(cancellationToken);
    }
}
