using MediatR;

namespace Portfolio.Application.Users.UpdateUser;

public sealed record UpdateUserRequest(
    Guid Id,
    string Email,
    string Password
) : IRequest<UpdateUserResponse>;
