using MediatR;

using Portfolio.Users.Responses;

namespace Portfolio.Users.Requests;

public sealed record SignInUserRequest(string Email, string Password) : IRequest<TokenResponse>;
