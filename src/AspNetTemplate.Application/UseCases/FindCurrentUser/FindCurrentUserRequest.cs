using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.FindCurrentUser;

public sealed record FindCurrentUserRequest : IRequest<FindCurrentUserResponse>;
