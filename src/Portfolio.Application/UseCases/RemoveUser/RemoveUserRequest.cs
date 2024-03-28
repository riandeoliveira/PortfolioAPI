using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.RemoveUser;

public sealed record RemoveUserRequest : IRequest<RemoveUserResponse>;
