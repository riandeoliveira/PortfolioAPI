using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.UpdateUser;

public sealed record UpdateUserRequest(string Email, string Password) : IRequest;
