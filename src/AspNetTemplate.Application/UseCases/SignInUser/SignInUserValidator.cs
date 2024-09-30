using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Common.Extensions;

using FluentValidation;

namespace AspNetTemplate.Application.UseCases.SignInUser;

public sealed class SignInUserValidator : AbstractValidator<SignInUserRequest>
{
    public SignInUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .Message(Message.EmailIsRequired)

            .MinimumLength(8)
            .Message(Message.MinimumEmailLength)

            .MaximumLength(64)
            .Message(Message.MaximumEmailLength)

            .EmailAddress()
            .Message(Message.EmailIsValid);

        RuleFor(x => x.Password)
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
