namespace Portfolio.Users.Responses;

public sealed record SignUpUserResponse(string Token, Guid UserId);
