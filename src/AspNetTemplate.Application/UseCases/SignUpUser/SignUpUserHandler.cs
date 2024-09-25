using Mapster;

using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Tools;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Enums;

namespace AspNetTemplate.Application.UseCases.SignUpUser;

public sealed class SignUpUserHandler(
    IAuthService authService,
    IPersonalRefreshTokenRepository personalRefreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<SignUpUserRequest>
{
    public async Task Handle(SignUpUserRequest request, CancellationToken cancellationToken = default)
    {
        bool userAlreadyExists = await userRepository.ExistAsync(
            x => x.Email == request.Email &&
            !x.DeletedAt.HasValue,
            cancellationToken
        );

        if (userAlreadyExists) throw new ConflictException(Message.EmailAlreadyExists);

        string hashedPassword = PasswordTool.Hash(request.Password);

        User user = new()
        {
            Email = request.Email,
            Password = hashedPassword
        };

        User createdUser = await userRepository.CreateAsync(user, cancellationToken);

        JwtTokenDto jwtTokenDto = authService.CreateJwtTokenData(createdUser.Adapt<UserDto>());

        PersonalRefreshToken personalRefreshToken = new()
        {
            Value = jwtTokenDto.RefreshToken.Value,
            ExpiresIn = jwtTokenDto.RefreshToken.ExpiresIn,
            UserId = createdUser.Id
        };

        await personalRefreshTokenRepository.CreateAsync(personalRefreshToken, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        authService.SendJwtCookiesToClient(jwtTokenDto);
    }
}
