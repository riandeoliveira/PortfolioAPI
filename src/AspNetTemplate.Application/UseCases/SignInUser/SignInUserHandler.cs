using Mapster;

using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Tools;

namespace AspNetTemplate.Application.UseCases.SignInUser;

public sealed class SignInUserHandler(
    IAuthService authService,
    IPersonalRefreshTokenRepository personalRefreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<SignInUserRequest>
{
    public async Task Handle(SignInUserRequest request, CancellationToken cancellationToken = default)
    {
        User? user = await userRepository.FindOneAsync(
            x => x.Email == request.Email,
            cancellationToken
        );

        if (user is null) throw new UnauthorizedException(Message.InvalidCredentials);

        bool isPasswordValid = PasswordTool.Verify(request.Password, user.Password);

        if (!isPasswordValid) throw new UnauthorizedException(Message.InvalidCredentials);

        PersonalRefreshToken? currentPersonalRefreshToken = await personalRefreshTokenRepository.FindOneAsync(
            x => x.UserId == user.Id &&
            !x.HasBeenUsed &&
            !x.DeletedAt.HasValue,
            cancellationToken
        );

        if (currentPersonalRefreshToken is not null)
        {
            currentPersonalRefreshToken.HasBeenUsed = true;

            await personalRefreshTokenRepository.UpdateAsync(currentPersonalRefreshToken, cancellationToken);
        }

        JwtTokenDto jwtTokenDto = authService.CreateJwtTokenData(user.Adapt<UserDto>());

        PersonalRefreshToken personalRefreshToken = new()
        {
            Value = jwtTokenDto.RefreshToken.Value,
            ExpiresIn = jwtTokenDto.RefreshToken.ExpiresIn,
            UserId = user.Id
        };

        await personalRefreshTokenRepository.CreateAsync(personalRefreshToken, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        authService.SendJwtCookiesToClient(jwtTokenDto);
    }
}
