namespace AspNetTemplate.Domain.Dtos;

public sealed record JwtTokenDto(
    Guid UserId,
    TokenDto AccessToken,
    TokenDto RefreshToken
);
