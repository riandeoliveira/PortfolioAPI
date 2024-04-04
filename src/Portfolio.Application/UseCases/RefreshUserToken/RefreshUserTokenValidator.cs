using FluentValidation;

namespace Portfolio.Application.UseCases.RefreshUserToken;

public sealed class RefreshUserTokenValidator : AbstractValidator<RefreshUserTokenRequest>
{
    public RefreshUserTokenValidator()
    {
    }
}
