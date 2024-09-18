namespace AspNetTemplate.Domain.Dtos;

public sealed record TokenDto(
    string AccessToken,
    string RefreshToken,
    long Expires,
    Guid UserId
);
