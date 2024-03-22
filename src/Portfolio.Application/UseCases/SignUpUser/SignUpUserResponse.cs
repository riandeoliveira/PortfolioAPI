namespace Portfolio.Application.UseCases.SignUpUser;

public sealed record SignUpUserResponse(string Token, Guid UserId);
