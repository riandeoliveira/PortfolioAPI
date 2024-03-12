using FluentValidation;

namespace Portfolio.Authors.Features.Remove;

public sealed class RemoveAuthorValidator : AbstractValidator<RemoveAuthorRequest>
{
    public RemoveAuthorValidator()
    {
    }
}
