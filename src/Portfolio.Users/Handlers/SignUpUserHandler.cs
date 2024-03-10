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
    IUserRepository userRepository,
    SignUpUserValidator validator
) : IRequestHandler<SignUpUserRequest, SignUpUserResponse>
{
    private readonly IAuthService _authService = authService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly SignUpUserValidator _validator = validator;

    public async Task<SignUpUserResponse> Handle(SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateRequestAsync(request, cancellationToken);

        string hashedPassword = PasswordExtension.HashPassword(request.Password);

        User user = new()
        {
            Email = request.Email,
            Password = hashedPassword
        };

        User createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);

        string token = _authService.GenerateToken(createdUser);

        return new SignUpUserResponse(token, createdUser.Id);
    }
}
