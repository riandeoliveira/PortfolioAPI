using FluentValidation;

namespace Portfolio.Application.UseCases.FindOneAuthor;

public sealed class FindOneAuthorValidator : AbstractValidator<FindOneAuthorRequest>
{
    public FindOneAuthorValidator()
    {
    }
}
