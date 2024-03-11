using FluentValidation;

using Portfolio.Authors.Interfaces;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Authors.Features.Update;

public sealed class UpdateAuthorValidator : AbstractValidator<UpdateAuthorRequest>
{
    public UpdateAuthorValidator(
        IAuthorRepository authorRepository,
        IAuthService authService,
        ILocalizationService localizationService
    )
    {
        RuleFor(request => request.Id)
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

        RuleFor(request => request.Name)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.NameIsRequired)

            .MaximumLength(64)
            .Message(localizationService, LocalizationMessages.MaximumNameLength);

        RuleFor(request => request.FullName)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.FullNameIsRequired)

            .MaximumLength(128)
            .Message(localizationService, LocalizationMessages.MaximumFullNameLength);

        RuleFor(request => request.Position)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.PositionIsRequired)

            .MaximumLength(64)
            .Message(localizationService, LocalizationMessages.MaximumPositionLength);

        RuleFor(request => request.Description)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.DescriptionIsRequired)

            .MaximumLength(1024)
            .Message(localizationService, LocalizationMessages.MaximumPositionLength);

        RuleFor(request => request.AvatarUrl)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.AvatarUrlIsRequired)

            .MaximumLength(512)
            .Message(localizationService, LocalizationMessages.MaximumAvatarUrlLength);

        RuleFor(request => request.SpotifyAccountName)
            .MaximumLength(64)
            .Message(localizationService, LocalizationMessages.MaximumSpotifyAccountNameLength);
    }
}
