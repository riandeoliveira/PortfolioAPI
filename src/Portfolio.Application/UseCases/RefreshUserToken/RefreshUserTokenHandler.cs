using Mapster;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.RefreshUserToken;

public sealed class RefreshUserTokenHandler(
    IAuthService authService
) : IRequestHandler<RefreshUserTokenRequest, RefreshUserTokenResponse>
{
    public async Task<RefreshUserTokenResponse> Handle(RefreshUserTokenRequest request, CancellationToken cancellationToken = default)
    {
        UserDto userDto = await authService.GetUserFromAccessTokenAsync(request.RefreshToken, cancellationToken);
        
        TokenDto tokenDto = await authService.GenerateTokenDataAsync(userDto, cancellationToken);

        return new RefreshUserTokenResponse(tokenDto);
    }
}
