using Portfolio.Users.Responses;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Requests;

public sealed record SignInUserRequest(string Email, string Password) : IRequest<TokenResponse>;
