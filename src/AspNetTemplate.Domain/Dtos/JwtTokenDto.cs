namespace AspNetTemplate.Domain.Dtos;

public sealed record JwtTokenDto(
    TokenDto AccessToken,
    TokenDto RefreshToken
);
