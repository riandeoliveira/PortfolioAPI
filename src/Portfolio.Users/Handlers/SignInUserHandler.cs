using FluentValidation;

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
    IUserRepository userRepository,
    SignInUserValidator validator
) : IRequestHandler<SignInUserRequest, SignInUserResponse>
{
    private readonly IAuthService _authService = authService;
    private readonly ILocalizationService _localizationService = localizationService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly SignInUserValidator _validator = validator;

    public async Task<SignInUserResponse> Handle(SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateRequestAsync(request, cancellationToken);

        User user = await _userRepository.FindByEmailOrThrowAsync(request.Email, cancellationToken);
        bool isValidPassword = PasswordExtension.VerifyPassword(request.Password, user.Password);

        if (!isValidPassword)
        {
            throw new ValidationException(_localizationService.GetKey(LocalizationMessages.InvalidLoginCredentials));
        }

        string token = _authService.GenerateToken(user);

        return new SignInUserResponse(token, user.Id);
    }
}
