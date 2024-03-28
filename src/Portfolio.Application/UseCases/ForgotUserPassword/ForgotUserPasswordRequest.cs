using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.ForgotUserPassword;

public sealed record ForgotUserPasswordRequest(string Email) : IRequest<ForgotUserPasswordResponse>;
