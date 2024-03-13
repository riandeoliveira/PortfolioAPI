using FluentValidation;

namespace Portfolio.Authors.Features.FindMany;

public sealed class FindManyAuthorsValidator : AbstractValidator<FindManyAuthorsRequest>
{
    public FindManyAuthorsValidator()
    {
    }
}
