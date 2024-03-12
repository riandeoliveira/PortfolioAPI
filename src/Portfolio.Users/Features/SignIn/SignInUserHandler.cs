using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Exceptions;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;
using Portfolio.Utils.Tools;

namespace Portfolio.Users.Features.SignIn;

public sealed class SignInUserHandler(
    IAuthService authService,
    ILocalizationService localizationService,
    IUserRepository userRepository,
    SignInUserValidator validator
) : IRequestHandler<SignInUserRequest, SignInUserResponse>
{
    public async Task<SignInUserResponse> Handle(SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(user => user.Email == request.Email, cancellationToken);
        bool isValidPassword = PasswordTool.Verify(request.Password, user.Password);

        if (!isValidPassword)
        {
            throw new BaseException(localizationService, LocalizationMessages.InvalidLoginCredentials);
        }

        string token = authService.GenerateToken(user);

        return new SignInUserResponse(token, user.Id);
    }
}
