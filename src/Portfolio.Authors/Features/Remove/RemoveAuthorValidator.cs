using FluentValidation;

using Portfolio.Authors.Interfaces;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Authors.Features.Remove;

public sealed class RemoveAuthorValidator : AbstractValidator<RemoveAuthorRequest>
{
    public RemoveAuthorValidator(
        IAuthorRepository authorRepository,
        ILocalizationService localizationService
    ) =>
        RuleFor(request => request.Id)
            .NotEmpty()
            .MustAsync(authorRepository.ExistAsync)
            .Message(localizationService, LocalizationMessages.AuthorNotFound);
}
