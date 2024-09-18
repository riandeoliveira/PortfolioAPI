using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.RemoveUser;

public sealed class RemoveUserHandler(
    IAuthService authService,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
{
    public async Task<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken = default)
    {
        UserDto userDto = await authService.GetCurrentUserAsync(cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(
            userDto.Id,
            cancellationToken
        );

        await userRepository.RemoveSoftAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new RemoveUserResponse();
    }
}
