using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Common.Extensions;

using FluentValidation;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

public sealed class ForgotUserPasswordValidator : AbstractValidator<ForgotUserPasswordRequest>
{
    public ForgotUserPasswordValidator()
        => RuleFor(x => x.Email)
            .NotEmpty()
            .Message(Message.EmailIsRequired)

            .MinimumLength(8)
            .Message(Message.MinimumEmailLength)

            .MaximumLength(64)
            .Message(Message.MaximumEmailLength)

            .EmailAddress()
            .Message(Message.EmailIsValid);
}
