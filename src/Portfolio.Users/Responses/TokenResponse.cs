namespace Portfolio.Users.Responses;

public record TokenResponse
{
    public required string Token { get; set; }

    public required Guid UserId { get; set; }
}
