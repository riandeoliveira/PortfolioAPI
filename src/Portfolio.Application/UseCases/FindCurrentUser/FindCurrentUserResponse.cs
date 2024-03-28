using Portfolio.Domain.Dtos;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed record FindCurrentUserResponse(UserDto User);
