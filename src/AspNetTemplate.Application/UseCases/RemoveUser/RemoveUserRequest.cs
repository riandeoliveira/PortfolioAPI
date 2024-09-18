using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.RemoveUser;

public sealed record RemoveUserRequest : IRequest<RemoveUserResponse>;
