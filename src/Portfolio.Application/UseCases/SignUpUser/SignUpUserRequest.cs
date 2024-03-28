using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.SignUpUser;

public sealed record SignUpUserRequest(string Email, string Password) : IRequest<SignUpUserResponse>;
