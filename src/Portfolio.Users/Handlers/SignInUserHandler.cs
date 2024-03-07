using System.Security.Authentication;

using FluentValidation;
using FluentValidation.Results;

using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Users.Validators;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Handlers;

public sealed class SignInUserHandler
(
    IAuthService authService,
    ILocalizationService localizationService,
    IUserRepository userRepository
) : IRequestHandler<SignInUserRequest, TokenResponse>
{
    private readonly IAuthService _authService = authService;
    private readonly ILocalizationService _localizationService = localizationService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<TokenResponse> Handle(SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        SignInUserValidator validator = new(_localizationService, _userRepository);
        ValidationResult result = await validator.ValidateAsync(request, cancellationToken);
        User? user = await _userRepository.FindByEmailAsync(request.Email, cancellationToken);

        if (user is null || !result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }

        bool isValidPassword = PasswordExtension.VerifyPassword(request.Password, user.Password);

        if (!isValidPassword)
        {
            throw new InvalidCredentialException(_localizationService.GetKey(LocalizationMessages.InvalidLoginCredentials));
        }

        string token = _authService.GenerateToken(user);

        return new TokenResponse(token, user.Id);
    }
}
