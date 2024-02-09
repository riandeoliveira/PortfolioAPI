using System.ComponentModel.DataAnnotations;

using MediatR;

using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Authors.Validators;

namespace Portfolio.Authors.Handlers;

public sealed class DeleteAuthorHandler(IAuthorRepository authorRepository) : IRequestHandler<DeleteAuthorRequest>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task Handle(DeleteAuthorRequest request, CancellationToken cancellationToken = default)
    {
        var validator = new DeleteAuthorValidator(_authorRepository);
        var result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }
    }
}
