using AspNetTemplate.Domain.Dtos;

namespace AspNetTemplate.Application.UseCases.FindCurrentUser;

public sealed record FindCurrentUserResponse(UserDto User);
