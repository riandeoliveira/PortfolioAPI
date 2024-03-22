using MediatR;

namespace Portfolio.Application.UseCases.SignInUser;

public sealed record SignInUserRequest(string Email, string Password) : IRequest<SignInUserResponse>;
