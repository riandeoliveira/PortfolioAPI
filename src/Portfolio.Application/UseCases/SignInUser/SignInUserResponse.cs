namespace Portfolio.Application.UseCases.SignInUser;

public sealed record SignInUserResponse(string Token, Guid UserId);
