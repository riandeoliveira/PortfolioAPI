using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.UpdateUser;

public sealed record UpdateUserRequest(string Email, string Password) : IRequest<UpdateUserResponse>;
