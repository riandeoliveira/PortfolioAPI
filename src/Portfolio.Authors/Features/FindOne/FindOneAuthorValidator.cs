using FluentValidation;

using Portfolio.Authors.Interfaces;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Authors.Features.FindOne;

public sealed class FindOneAuthorValidator : AbstractValidator<FindOneAuthorRequest>
{
    public FindOneAuthorValidator(
        IAuthorRepository authorRepository,
        IAuthService authService,
        ILocalizationService localizationService
    ) => RuleFor(request => request.Id)
            .NotEmpty()
            .MustAsync(authorRepository.ExistAsync)
            .Message(localizationService, LocalizationMessages.AuthorNotFound)

            .MustAsync(async (id, cancellationToken) =>
                await authorRepository.ExistAsync(
                    author => author.Id == id &&
                    author.UserId == authService.GetLoggedInUserId(),
                    cancellationToken
                )
            )
            .Message(localizationService, LocalizationMessages.UnauthorizedOperation);
}
