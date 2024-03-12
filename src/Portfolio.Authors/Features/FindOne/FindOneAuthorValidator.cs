using FluentValidation;

namespace Portfolio.Authors.Features.FindOne;

public sealed class FindOneAuthorValidator : AbstractValidator<FindOneAuthorRequest>
{
    public FindOneAuthorValidator()
    {
    }
}
