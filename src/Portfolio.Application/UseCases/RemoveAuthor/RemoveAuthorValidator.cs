using FluentValidation;

namespace Portfolio.Application.UseCases.RemoveAuthor;

public sealed class RemoveAuthorValidator : AbstractValidator<RemoveAuthorRequest>
{
    public RemoveAuthorValidator()
    {
    }
}
