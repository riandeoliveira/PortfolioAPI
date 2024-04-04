using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.RefreshUserToken;

public sealed record RefreshUserTokenRequest(string Token, string RefreshToken) : IRequest<RefreshUserTokenResponse>;
