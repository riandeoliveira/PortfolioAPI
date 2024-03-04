using System.ComponentModel.DataAnnotations;

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
        var validator = new SignUpUserValidator(_localizationService, _userRepository);
        var result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }

        var hashedPassword = PasswordExtension.HashPassword(request.Password);

        var newUser = new User
        {
            Email = request.Email,
            Password = hashedPassword
        };

        var createdUser = await _userRepository.CreateAsync(newUser, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);

        var token = _authService.GenerateToken(createdUser);

        return new TokenResponse
        {
            Token = token,
            UserId = createdUser.Id
        };
    }
}
