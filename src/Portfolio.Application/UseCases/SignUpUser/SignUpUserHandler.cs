using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Tools;

namespace Portfolio.Application.UseCases.SignUpUser;

public sealed class SignUpUserHandler(
    IAuthService authService,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<SignUpUserRequest, SignUpUserResponse>
{
    public async Task<SignUpUserResponse> Handle(SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        string hashedPassword = PasswordTool.Hash(request.Password);

        User user = new()
        {
            Email = request.Email,
            Password = hashedPassword
        };

        User createdUser = await userRepository.CreateAsync(user, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        TokenDto tokenDto = await authService.GenerateTokenDataAsync(
            createdUser.Adapt<UserDto>(),
            cancellationToken
        );

        return new SignUpUserResponse(tokenDto);
    }
}
