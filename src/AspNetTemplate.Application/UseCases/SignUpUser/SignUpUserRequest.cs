using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.SignUpUser;

public sealed record SignUpUserRequest(string Email, string Password) : IRequest<SignUpUserResponse>;
