using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Utils.Enums;
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
        var user = await _userRepository.FindAsync(user =>
            user.Email == request.Email,
            cancellationToken
        );

        if (user is not null)
        {
            throw new Exception(_localizationService.GetKey(LocalizationKeys.ExistingEmailMessage));
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
