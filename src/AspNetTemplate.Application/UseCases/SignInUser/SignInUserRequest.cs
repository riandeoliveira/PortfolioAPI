using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.SignInUser;

public sealed record SignInUserRequest(string Email, string Password) : IRequest<SignInUserResponse>;
