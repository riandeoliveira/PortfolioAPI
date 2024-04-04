using Portfolio.Domain.Dtos;

namespace Portfolio.Application.UseCases.RefreshUserToken;

public sealed record RefreshUserTokenResponse(TokenDto TokenDto);
