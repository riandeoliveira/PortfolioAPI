using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.UpdateUser;

public sealed class UpdateUserHandler(
    IAuthService authService,
    IUserRepository userRepository
) : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindOneOrThrowAsync(
            user => user.Id == request.Id &&
            user.Id == authService.GetLoggedInUserId(),
            cancellationToken
        );

        user.Email = request.Email.Trim();
        user.Password = request.Password.Trim();

        await userRepository.UpdateAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse();
    }
}
