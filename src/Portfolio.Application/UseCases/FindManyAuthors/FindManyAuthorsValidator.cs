using FluentValidation;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed class FindManyAuthorsValidator : AbstractValidator<FindManyAuthorsRequest>
{
    public FindManyAuthorsValidator()
    {
    }
}
