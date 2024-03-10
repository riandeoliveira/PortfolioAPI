namespace Portfolio.Users.Responses;

public sealed record SignInUserResponse(string Token, Guid UserId);
