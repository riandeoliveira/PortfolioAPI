using FluentValidation;

using Microsoft.Extensions.Localization;

using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Authors.Validators;

public sealed class RemoveAuthorValidator : AbstractValidator<RemoveAuthorRequest>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ILocalizationService _localizationService;

    public RemoveAuthorValidator(IAuthorRepository authorRepository, ILocalizationService localizationService)
    {
        _authorRepository = authorRepository;
        _localizationService = localizationService;

        RuleFor(request => request.Id)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken) =>
                await _authorRepository.ExistAsync(id, cancellationToken)
            )
            .WithMessage(_localizationService.GetKey(LocalizationMessages.AuthorNotFound));
    }
}
