using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.RefreshUserToken;

public sealed record RefreshUserTokenRequest(string AccessToken, string RefreshToken) : IRequest<RefreshUserTokenResponse>;
