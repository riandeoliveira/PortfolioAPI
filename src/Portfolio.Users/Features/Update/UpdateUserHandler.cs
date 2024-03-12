using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Features.Update;

public sealed class UpdateUserHandler(
    IAuthService authService,
    IUserRepository userRepository,
    UpdateUserValidator validator
) : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(
            user => user.Id == request.Id &&
            user.Id == authService.GetLoggedInUserId(),
            cancellationToken
        );

        user.Email = request.Email;
        user.Password = request.Password;
        user.UpdatedAt = DateTime.Now;

        await userRepository.UpdateAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse();
    }
}
