using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.RefreshUserToken;

public sealed record RefreshUserTokenRequest(string AccessToken, string RefreshToken) : IRequest<RefreshUserTokenResponse>;
