using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.FindCurrentUser;

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
