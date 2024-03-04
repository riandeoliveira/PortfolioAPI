using FluentValidation;
using FluentValidation.Results;

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
    ILocalizationService localizationService,
    IUserRepository userRepository
) : IRequestHandler<SignUpUserRequest, TokenResponse>
{
    private readonly IAuthService _authService = authService;
    private readonly ILocalizationService _localizationService = localizationService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<TokenResponse> Handle(SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        SignUpUserValidator validator = new(_localizationService, _userRepository);
        ValidationResult result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }

        string hashedPassword = PasswordExtension.HashPassword(request.Password);

        User newUser = new()
        {
            Email = request.Email,
            Password = hashedPassword
        };

        User createdUser = await _userRepository.CreateAsync(newUser, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);

        string token = _authService.GenerateToken(createdUser);

        return new TokenResponse(token, createdUser.Id);
    }
}
