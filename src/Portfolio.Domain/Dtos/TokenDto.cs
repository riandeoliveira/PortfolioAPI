namespace Portfolio.Domain.Dtos;

public sealed record TokenDto(
    string Token,
    string RefreshToken,
    long Expires,
    Guid UserId
);
