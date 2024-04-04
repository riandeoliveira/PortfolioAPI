using Portfolio.Domain.Dtos;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed class FindCurrentUserHandler(
    IAuthService authService
) : IRequestHandler<FindCurrentUserRequest, FindCurrentUserResponse>
{
    public async Task<FindCurrentUserResponse> Handle(FindCurrentUserRequest request, CancellationToken cancellationToken = default)
    {
        UserDto userDto = await authService.GetCurrentUserAsync(cancellationToken);

        return new FindCurrentUserResponse(userDto);
    }
}
