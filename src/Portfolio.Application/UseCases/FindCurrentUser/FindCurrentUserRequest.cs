using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed record FindCurrentUserRequest : IRequest<FindCurrentUserResponse>;
