namespace AspNetTemplate.Infra.Data.Dtos;

public sealed record JwtTokenDto(
    TokenDto AccessToken,
    TokenDto RefreshToken
);
