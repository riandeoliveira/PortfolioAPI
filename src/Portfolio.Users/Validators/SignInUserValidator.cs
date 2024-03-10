using FluentValidation;

using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Users.Validators;

public sealed class SignInUserValidator : AbstractValidator<SignInUserRequest>
{
    public SignInUserValidator(ILocalizationService localizationService, IUserRepository userRepository)
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
                await userRepository.ExistAsync(user => user.Email == email, cancellationToken)
            )
            .Message(localizationService, LocalizationMessages.EmailIsNotRegistered);

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
