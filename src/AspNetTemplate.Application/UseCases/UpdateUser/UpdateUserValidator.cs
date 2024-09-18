using FluentValidation;

using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Extensions;

namespace AspNetTemplate.Application.UseCases.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator(
        IAuthService authService,
        IUserRepository userRepository
    )
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
            {
                UserDto currentUser = await authService.GetCurrentUserAsync(cancellationToken);

                bool emailAlreadyExists = await userRepository.ExistAsync(
                    user => user.Email == email &&
                    user.Id != currentUser.Id,
                    cancellationToken
                );

                return !emailAlreadyExists;
            })
            .Message(Message.EmailAlreadyExists);

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
