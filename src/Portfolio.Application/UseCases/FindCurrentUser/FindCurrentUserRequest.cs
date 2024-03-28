using MediatR;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed record FindCurrentUserRequest : IRequest<FindCurrentUserResponse>;
