namespace AspNetTemplate.Dtos;

public record AuthTokensDto(JwtTokenDto AccessToken, JwtTokenDto RefreshToken);
