using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.SignUpUser;

public sealed class SignUpUserValidator : AbstractValidator<SignUpUserRequest>
{
    public SignUpUserValidator(ILocalizationService localizationService, IUserRepository userRepository)
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
                !await userRepository.ExistAsync(user => user.Email == email, cancellationToken)
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
