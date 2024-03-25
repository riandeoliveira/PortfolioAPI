using MediatR;

namespace Portfolio.Application.UseCases.RemoveUser;

public sealed record RemoveUserRequest : IRequest<RemoveUserResponse>;
