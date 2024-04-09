using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.UpdateUser;

public sealed class UpdateUserHandler(
    IAuthService authService,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        UserDto currentUser = await authService.GetCurrentUserAsync(cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(currentUser.Id, cancellationToken);

        user.Email = request.Email;
        user.Password = request.Password;

        await userRepository.UpdateAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new UpdateUserResponse();
    }
}
