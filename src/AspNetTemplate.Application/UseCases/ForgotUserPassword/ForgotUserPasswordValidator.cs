using FluentValidation;

using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infrastructure.Extensions;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

public sealed class ForgotUserPasswordValidator : AbstractValidator<ForgotUserPasswordRequest>
{
    public ForgotUserPasswordValidator()
        => RuleFor(request => request.Email)
            .NotEmpty()
            .Message(Message.EmailIsRequired)

            .MinimumLength(8)
            .Message(Message.MinimumEmailLength)

            .MaximumLength(64)
            .Message(Message.MaximumEmailLength)

            .EmailAddress()
            .Message(Message.EmailIsValid);
}
