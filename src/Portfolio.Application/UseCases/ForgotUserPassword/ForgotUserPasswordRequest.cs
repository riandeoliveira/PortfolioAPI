using MediatR;

namespace Portfolio.Application.UseCases.ForgotUserPassword;

public sealed record ForgotUserPasswordRequest(string Email) : IRequest<ForgotUserPasswordResponse>;
