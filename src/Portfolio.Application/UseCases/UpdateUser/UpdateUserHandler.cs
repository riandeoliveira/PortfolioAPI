using MediatR;

using Portfolio.Application.UseCases.UpdateUser;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.Users.UpdateUser;

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

        await userRepository.UpdateAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse();
    }
}
