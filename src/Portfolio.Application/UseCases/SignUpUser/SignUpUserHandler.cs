using Mapster;

using MediatR;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;
using Portfolio.Infrastructure.Tools;

namespace Portfolio.Application.UseCases.SignUpUser;

public sealed class SignUpUserHandler(
    IAuthService authService,
    IUserRepository userRepository,
    SignUpUserValidator validator
) : IRequestHandler<SignUpUserRequest, SignUpUserResponse>
{
    public async Task<SignUpUserResponse> Handle(SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

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
