using Portfolio.Users.Responses;
using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Requests;

public sealed record SignUpUserRequest(string Email, string Password) : IRequest<TokenResponse>;
