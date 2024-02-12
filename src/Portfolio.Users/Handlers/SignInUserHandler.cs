using Microsoft.Extensions.Configuration;

using Portfolio.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Handlers;

public sealed class SignInUserHandler(IAuthService authService, IUserRepository userRepository) : IRequestHandler<SignInUserRequest, TokenResponse>
{
    private readonly IAuthService _authService = authService;

    private readonly IUserRepository _userRepository = userRepository;

    public async Task<TokenResponse> Handle(SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.FindAsync(user =>
            user.Email == request.Email,
            cancellationToken
        );

        if (user is not null)
        {
            throw new Exception("Email already exists");
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
