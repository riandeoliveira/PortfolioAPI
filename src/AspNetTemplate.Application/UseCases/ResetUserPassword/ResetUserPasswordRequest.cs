using AspNetTemplate.Domain.Interfaces;

namespace AspNetTemplate.Application.UseCases.ResetUserPassword;

public sealed record ResetUserPasswordRequest(
    string Password,
    string PasswordConfirmation
) : IRequest;
