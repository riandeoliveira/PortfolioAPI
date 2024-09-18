using FluentValidation;

using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infrastructure.Extensions;

namespace AspNetTemplate.Application.UseCases.ResetUserPassword;

public sealed class ResetUserPasswordValidator : AbstractValidator<ResetUserPasswordRequest>
{
    public ResetUserPasswordValidator()
    {
        RuleFor(request => request)
            .Must(request => request.Password == request.PasswordConfirmation)
            .Message(Message.EquivalentPasswords);

        RuleFor(request => request.Password)
            .NotEmpty()
            .Message(Message.PasswordIsRequired)

            .MinimumLength(8)
            .Message(Message.MinimumPasswordLength)

            .MaximumLength(64)
            .Message(Message.MaximumPasswordLength)

            .StrongPassword()
            .Message(Message.StrongPassword);

        RuleFor(request => request.PasswordConfirmation)
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
