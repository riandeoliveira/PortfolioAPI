using FluentValidation;

namespace AspNetTemplate.Application.UseCases.RefreshUserToken;

public sealed class RefreshUserTokenValidator : AbstractValidator<RefreshUserTokenRequest>
{
    public RefreshUserTokenValidator()
    {
    }
}
