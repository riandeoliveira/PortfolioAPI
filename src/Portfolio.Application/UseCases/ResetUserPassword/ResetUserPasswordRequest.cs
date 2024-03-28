using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.ResetUserPassword;

public sealed record ResetUserPasswordRequest(
    string Password,
    string PasswordConfirmation
) : IRequest<ResetUserPasswordResponse>;
