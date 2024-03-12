using FluentValidation;

using Portfolio.Users.Interfaces;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Users.Features.Update;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator(
        IAuthService authService,
        ILocalizationService localizationService,
        IUserRepository userRepository
    )
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.EmailIsRequired)

            .MinimumLength(8)
            .Message(localizationService, LocalizationMessages.MinimumEmailLength)

            .MaximumLength(64)
            .Message(localizationService, LocalizationMessages.MaximumEmailLength)

            .EmailAddress()
            .Message(localizationService, LocalizationMessages.InvalidEmail)

            .MustAsync(async (email, cancellationToken) =>
                !await userRepository.ExistAsync(
                    user => user.Email == email &&
                    user.Id != authService.GetLoggedInUserId(),
                    cancellationToken
                )
            )
            .Message(localizationService, LocalizationMessages.EmailAlreadyExists);

        RuleFor(request => request.Password)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.PasswordIsRequired)

            .MinimumLength(8)
            .Message(localizationService, LocalizationMessages.MinimumPasswordLength)

            .MaximumLength(64)
            .Message(localizationService, LocalizationMessages.MaximumPasswordLength)

            .StrongPassword()
            .Message(localizationService, LocalizationMessages.StrongPassword);
    }
}
