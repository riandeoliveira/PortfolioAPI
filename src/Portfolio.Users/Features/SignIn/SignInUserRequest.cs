using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Features.SignIn;

public sealed record SignInUserRequest(string Email, string Password) : IRequest<SignInUserResponse>;
