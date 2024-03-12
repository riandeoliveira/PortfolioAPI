using Portfolio.Utils.Messaging;

namespace Portfolio.Users.Features.Update;

public sealed record UpdateUserRequest(
    Guid Id,
    string Email,
    string Password
) : IRequest<UpdateUserResponse>;
