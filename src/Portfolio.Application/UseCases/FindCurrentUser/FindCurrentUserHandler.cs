using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed class FindCurrentUserHandler(
    IAuthService authService,
    IUserRepository userRepository
) : IRequestHandler<FindCurrentUserRequest, FindCurrentUserResponse>
{
    public async Task<FindCurrentUserResponse> Handle(FindCurrentUserRequest request, CancellationToken cancellationToken = default)
    {
        User user = await userRepository.FindOneOrThrowAsync(
            authService.GetLoggedInUserId(),
            cancellationToken
        );

        return new FindCurrentUserResponse(user.Adapt<UserDto>());
    }
}
