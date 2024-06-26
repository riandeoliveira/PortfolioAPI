using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Tools;

namespace Portfolio.Application.UseCases.SignInUser;

public sealed class SignInUserHandler(
    IAuthService authService,
    IUserRepository userRepository
) : IRequestHandler<SignInUserRequest, SignInUserResponse>
{
    public async Task<SignInUserResponse> Handle(SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindOneOrThrowAsync(user => user.Email == request.Email, cancellationToken);

        bool isValidPassword = PasswordTool.Verify(request.Password, user.Password);

        if (!isValidPassword) throw new BaseException(Message.InvalidLoginCredentials);

        TokenDto tokenDto = await authService.GenerateTokenDataAsync(user.Adapt<UserDto>(), cancellationToken);

        return new SignInUserResponse(tokenDto);
    }
}
