using AspNetTemplate.Application.Interfaces;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Data.Dtos;
using AspNetTemplate.Infra.Data.Exceptions;
using AspNetTemplate.Infra.Data.Interfaces;

namespace AspNetTemplate.Application.UseCases.RenewUserRefreshToken;

public sealed class RenewUserRefreshTokenHandler(
    IAuthService authService,
    IPersonalRefreshTokenRepository personalRefreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<RenewUserRefreshTokenRequest>
{
    public async Task Handle(RenewUserRefreshTokenRequest _, CancellationToken cancellationToken = default)
    {
        (string? _, string? refreshToken) = authService.GetJwtCookies();

        authService.ValidateJwtTokenOrThrow(refreshToken);

        User user = await userRepository.FindAuthenticatedOrThrowAsync(cancellationToken);

        PersonalRefreshToken? currentPersonalRefreshToken = await personalRefreshTokenRepository.FindOneAsync(
            x => x.Value == refreshToken &&
            !x.DeletedAt.HasValue,
            cancellationToken
        );

        if (currentPersonalRefreshToken is null || currentPersonalRefreshToken.HasBeenUsed)
        {
            throw new UnauthorizedException(Message.UnauthorizedOperation);
        }

        currentPersonalRefreshToken.HasBeenUsed = true;

        await personalRefreshTokenRepository.UpdateAsync(currentPersonalRefreshToken, cancellationToken);

        JwtTokenDto jwtTokenDto = authService.CreateJwtTokenData(user.Id);

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
