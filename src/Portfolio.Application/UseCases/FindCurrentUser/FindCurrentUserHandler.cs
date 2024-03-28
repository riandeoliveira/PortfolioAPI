using Mapster;

using MediatR;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed class FindCurrentUserHandler(
    FindCurrentUserValidator validator,
    IAuthService authService,
    IUserRepository userRepository
) : IRequestHandler<FindCurrentUserRequest, FindCurrentUserResponse>
{
    public async Task<FindCurrentUserResponse> Handle(FindCurrentUserRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        User user = await userRepository.FindOneOrThrowAsync(
            authService.GetLoggedInUserId(),
            cancellationToken
        );

        return new FindCurrentUserResponse(user.Adapt<UserDto>());
    }
}
