using Mapster;

using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Tools;

namespace AspNetTemplate.Application.UseCases.SignInUser;

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
