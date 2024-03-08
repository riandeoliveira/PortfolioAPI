using FluentValidation;

using Portfolio.Authors.Requests;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Authors.Validators;

public sealed class CreateAuthorValidator : AbstractValidator<CreateAuthorRequest>
{
    private readonly ILocalizationService _localizationService;

    public CreateAuthorValidator(ILocalizationService localizationService)
    {
        _localizationService = localizationService;

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.NameIsRequired))

            .MaximumLength(64)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumNameLength));

        RuleFor(request => request.FullName)
            .NotEmpty()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.FullNameIsRequired))

            .MaximumLength(128)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumFullNameLength));

        RuleFor(request => request.Position)
            .NotEmpty()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.PositionIsRequired))

            .MaximumLength(64)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumPositionLength));

        RuleFor(request => request.Description)
            .NotEmpty()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.DescriptionIsRequired))

            .MaximumLength(1024)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumPositionLength));

        RuleFor(request => request.AvatarUrl)
            .NotEmpty()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.AvatarUrlIsRequired))

            .MaximumLength(512)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumAvatarUrlLength));

        RuleFor(request => request.SpotifyAccountName)
            .MaximumLength(64)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumSpotifyAccountNameLength));
    }
}
