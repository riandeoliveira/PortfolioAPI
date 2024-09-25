using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.DeleteUser;

public sealed class DeleteUserHandler(
    IAuthService authService,
    IPersonalRefreshTokenRepository personalRefreshTokenRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<DeleteUserRequest>
{
    public async Task Handle(DeleteUserRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindAuthenticatedOrThrowAsync(cancellationToken);

        await userRepository.SoftDeleteAsync(user, cancellationToken);
        await personalRefreshTokenRepository.SoftDeleteManyAsync(x => x.UserId == user.Id, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        authService.ClearJwtCookies();
    }
}
