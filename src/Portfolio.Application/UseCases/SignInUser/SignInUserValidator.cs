using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.SignInUser;

public sealed class SignInUserValidator : AbstractValidator<SignInUserRequest>
{
    public SignInUserValidator(IUserRepository userRepository)
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .Message(Message.EmailIsRequired)

            .MinimumLength(8)
            .Message(Message.MinimumEmailLength)

            .MaximumLength(64)
            .Message(Message.MaximumEmailLength)

            .EmailAddress()
            .Message(Message.InvalidEmail)

            .MustAsync(async (email, cancellationToken) =>
                await userRepository.ExistAsync(user => user.Email == email, cancellationToken)
            )
            .Message(Message.EmailIsNotRegistered);

        RuleFor(request => request.Password)
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
