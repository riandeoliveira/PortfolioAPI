using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Tools;

namespace Portfolio.Application.UseCases.SignUpUser;

public sealed class SignUpUserHandler(
    IAuthService authService,
    IUserRepository userRepository
) : IRequestHandler<SignUpUserRequest, SignUpUserResponse>
{
    public async Task<SignUpUserResponse> Handle(SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        string hashedPassword = PasswordTool.Hash(request.Password.Trim());

        User user = new()
        {
            Email = request.Email.Trim(),
            Password = hashedPassword
        };

        User createdUser = await userRepository.CreateAsync(user, cancellationToken);

        await userRepository.SaveChangesAsync(cancellationToken);

        string token = authService.GenerateToken(createdUser.Adapt<UserDto>());

        return new SignUpUserResponse(token, createdUser.Id);
    }
}
