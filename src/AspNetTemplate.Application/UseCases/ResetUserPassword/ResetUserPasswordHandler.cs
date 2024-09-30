using AspNetTemplate.Application.Interfaces;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Data.Dtos;
using AspNetTemplate.Infra.Data.Exceptions;
using AspNetTemplate.Infra.Data.Interfaces;
using AspNetTemplate.Infra.Data.Utilities;

namespace AspNetTemplate.Application.UseCases.ResetUserPassword;

public sealed class ResetUserPasswordHandler(
    IAuthService authService,
    IPersonalRefreshTokenRepository personalRefreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<ResetUserPasswordRequest>
{
    public async Task Handle(ResetUserPasswordRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindAuthenticatedOrThrowAsync(cancellationToken);

        string hashedPassword = PasswordUtility.Hash(request.Password);

        user.Password = hashedPassword;

        await userRepository.UpdateAsync(user, cancellationToken);

        PersonalRefreshToken? currentPersonalRefreshToken = await personalRefreshTokenRepository.FindOneAsync(
            x => x.UserId == user.Id &&
            !x.HasBeenUsed &&
            !x.DeletedAt.HasValue,
            cancellationToken
        );

        if (currentPersonalRefreshToken is null) throw new UnauthorizedException(Message.UnauthorizedOperation);

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
