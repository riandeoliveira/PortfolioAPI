using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.ForgotUserPassword;

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
            .Message(Message.InvalidEmail);
}
