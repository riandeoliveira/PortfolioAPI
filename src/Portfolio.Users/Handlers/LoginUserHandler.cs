using System.Security.Authentication;

using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Handlers;

public sealed class LoginUserHandler(IAuthService authService, IUserRepository repository) : IRequestHandler<LoginUserRequest, TokenResponse>
{
    private readonly IAuthService _authService = authService;

    private readonly IUserRepository _repository = repository;

    public async Task<TokenResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _repository.FindAsync(x =>
            x.Email == request.Email
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
