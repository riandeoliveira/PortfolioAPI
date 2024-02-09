using Microsoft.Extensions.Configuration;

using Portfolio.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Users.Responses;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Handlers;

public sealed class SignInUserHandler(IAuthService authService, IUserRepository repository, IConfiguration configuration) : IRequestHandler<SignInUserRequest, TokenResponse>
{
    private readonly IAuthService _authService = authService;

    private readonly IUserRepository _repository = repository;

    private readonly IConfiguration _configuration = configuration;

    public async Task<TokenResponse> Handle(SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        var userFound = await _repository.FindAsync(user =>
            user.Email == request.Email,
            cancellationToken
        );

        if (userFound is not null)
        {
            throw new Exception("Email already exists");
        }

        var hashedPassword = PasswordExtension.HashPassword(request.Password);

        var user = new User
        {
            Email = request.Email,
            Password = hashedPassword
        };

        var createdUser = await _repository.CreateAsync(user, cancellationToken);

        await _repository.SaveChangesAsync();

        var token = _authService.GenerateToken(createdUser);

        return new TokenResponse
        {
            Token = token,
            UserId = createdUser.Id
        };
    }
}
