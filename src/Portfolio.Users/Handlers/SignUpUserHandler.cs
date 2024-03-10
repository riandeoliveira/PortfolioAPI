using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Users.Validators;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Handlers;

public sealed class SignUpUserHandler
(
    IAuthService authService,
    IUserRepository userRepository,
    SignUpUserValidator validator
) : IRequestHandler<SignUpUserRequest, SignUpUserResponse>
{
    public async Task<SignUpUserResponse> Handle(SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateRequestAsync(request, cancellationToken);

        string hashedPassword = PasswordExtension.HashPassword(request.Password.Trim());

        User user = new()
        {
            Email = request.Email.Trim(),
            Password = hashedPassword
        };

        User createdUser = await userRepository.CreateAsync(user, cancellationToken);

        await userRepository.SaveChangesAsync(cancellationToken);

        string token = authService.GenerateToken(createdUser);

        return new SignUpUserResponse(token, createdUser.Id);
    }
}
