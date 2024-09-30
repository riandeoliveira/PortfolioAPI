using AspNetTemplate.Application.Interfaces;
using AspNetTemplate.Infra.Data.Interfaces;

namespace AspNetTemplate.Application.UseCases.VerifyUser;

public sealed class VerifyUserHandler(
    IUserRepository userRepository
) : IRequestHandler<VerifyUserRequest>
{
    public async Task Handle(VerifyUserRequest _, CancellationToken cancellationToken = default)
    {
        await userRepository.FindAuthenticatedOrThrowAsync(cancellationToken);
    }
}
