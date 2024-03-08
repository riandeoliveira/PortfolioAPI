using FluentValidation;

using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;

namespace Portfolio.Authors.Validators;

// TODO: APRIMORAR VALIDAÇÕES !!!

public sealed class RemoveAuthorValidator : AbstractValidator<RemoveAuthorRequest>
{
    private readonly IAuthorRepository _authorRepository;

    public RemoveAuthorValidator(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(AuthorMustExist)
            .WithMessage("Author not found");
    }

    private async Task<bool> AuthorMustExist(Guid id, CancellationToken cancellationToken = default)
    {
        return await _authorRepository.ExistAsync(id, cancellationToken);
    }
}
