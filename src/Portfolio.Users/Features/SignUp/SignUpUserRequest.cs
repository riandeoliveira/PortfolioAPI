using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Features.SignUp;

public sealed record SignUpUserRequest(string Email, string Password) : IRequest<SignUpUserResponse>;
