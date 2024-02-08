using MediatR;

using Portfolio.Users.Responses;

namespace Portfolio.Users.Requests;

public sealed record LoginUserRequest(string Email, string Password): IRequest<TokenResponse>;
