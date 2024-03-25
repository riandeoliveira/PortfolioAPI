using MediatR;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;
using Portfolio.Infrastructure.Tools;

namespace Portfolio.Application.UseCases.SignInUser;

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