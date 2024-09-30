using AspNetTemplate.Application.Interfaces;

namespace AspNetTemplate.Application.UseCases.ForgotUserPassword;

public sealed record ForgotUserPasswordRequest(string Email) : IRequest;
