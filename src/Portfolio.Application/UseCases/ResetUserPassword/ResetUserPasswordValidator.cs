using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.ResetUserPassword;

public sealed class ResetUserPasswordValidator : AbstractValidator<ResetUserPasswordRequest>
{
    public ResetUserPasswordValidator(ILocalizationService localizationService)
    {
        RuleFor(request => request)
            .Must(request => request.Password == request.PasswordConfirmation)
            .Message(localizationService, LocalizationMessages.EquivalentPasswords);

        RuleFor(request => request.Password)
            .NotEmpty()
            .Message(localizationService, LocalizationMessages.PasswordIsRequired)

            .MinimumLength(8)
            .Message(localizationService, LocalizationMessages.MinimumPasswordLength)

            .MaximumLength(64)
            .Message(localizationService, LocalizationMessages.MaximumPasswordLength)

            .StrongPassword()
            .Message(localizationService, LocalizationMessages.StrongPassword);

        RuleFor(request => request.PasswordConfirmation)
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
