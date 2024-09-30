using AspNetTemplate.Application.Interfaces;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Data.Exceptions;
using AspNetTemplate.Infra.Data.Interfaces;

namespace AspNetTemplate.Application.UseCases.SignOutUser;

public sealed class SignOutUserHandler(
    IAuthService authService,
    IPersonalRefreshTokenRepository personalRefreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<SignOutUserRequest>
{
    public async Task Handle(SignOutUserRequest _, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindAuthenticatedOrThrowAsync(cancellationToken);

        PersonalRefreshToken? personalRefreshToken = await personalRefreshTokenRepository.FindOneAsync(
            x => x.UserId == user.Id &&
            !x.HasBeenUsed &&
            !x.DeletedAt.HasValue,
            cancellationToken
        );

        if (personalRefreshToken is null) throw new UnauthorizedException(Message.UnauthorizedOperation);

        personalRefreshToken.HasBeenUsed = true;

        await personalRefreshTokenRepository.UpdateAsync(personalRefreshToken, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        authService.ClearJwtCookies();
    }
}
