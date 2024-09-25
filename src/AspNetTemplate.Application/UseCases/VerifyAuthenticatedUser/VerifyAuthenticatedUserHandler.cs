using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.VerifyAuthenticatedUser;

public sealed class VerifyAuthenticatedUserHandler(
    IUserRepository userRepository
) : IRequestHandler<VerifyAuthenticatedUserRequest>
{
    public async Task Handle(VerifyAuthenticatedUserRequest _, CancellationToken cancellationToken = default)
    {
        await userRepository.FindAuthenticatedOrThrowAsync(cancellationToken);
    }
}
