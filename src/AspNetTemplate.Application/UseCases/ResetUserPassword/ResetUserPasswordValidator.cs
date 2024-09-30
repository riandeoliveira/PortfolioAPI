using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Common.Extensions;

using FluentValidation;

namespace AspNetTemplate.Application.UseCases.ResetUserPassword;

public sealed class ResetUserPasswordValidator : AbstractValidator<ResetUserPasswordRequest>
{
    public ResetUserPasswordValidator()
    {
        RuleFor(x => x)
            .Must(x => x.Password == x.PasswordConfirmation)
            .Message(Message.EquivalentPasswords);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Message(Message.PasswordIsRequired)

            .MinimumLength(8)
            .Message(Message.MinimumPasswordLength)

            .MaximumLength(64)
            .Message(Message.MaximumPasswordLength)

            .StrongPassword()
            .Message(Message.StrongPassword);

        RuleFor(x => x.PasswordConfirmation)
            .NotEmpty()
            .Message(Message.PasswordIsRequired)

            .MinimumLength(8)
            .Message(Message.MinimumPasswordLength)

            .MaximumLength(64)
            .Message(Message.MaximumPasswordLength)

            .StrongPassword()
            .Message(Message.StrongPassword);
    }
}
