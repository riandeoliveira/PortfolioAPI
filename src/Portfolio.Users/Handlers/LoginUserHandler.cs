using System.Security.Authentication;

using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Handlers;

public sealed class LoginUserHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<LoginUserRequest, TokenResponse>
{
    private readonly IAuthService _authService = authService;

    private readonly IUserRepository _userRepository = userRepository;

    public async Task<TokenResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindAsync(user =>
            user.Email == request.Email,
            cancellationToken
        );

        if (user is not null && PasswordExtension.VerifyPassword(request.Password, user.Password))
        {
            var token = _authService.GenerateToken(user);

            return new TokenResponse
            {
                Token = token,
                UserId = user.Id
            };
        }

        throw new InvalidCredentialException("Invalid email or password");
    }
}
