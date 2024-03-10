using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Authors.Validators;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class RemoveAuthorHandler
(
    IAuthorRepository authorRepository,
    RemoveAuthorValidator validator
) : IRequestHandler<RemoveAuthorRequest>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly RemoveAuthorValidator _validator = validator;

    public async Task Handle(RemoveAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await _validator.ValidateRequestAsync(request, cancellationToken);

        Author? author = await _authorRepository.FindAsync(request.Id, cancellationToken);

        await _authorRepository.RemoveAsync(author, cancellationToken);
        await _authorRepository.SaveChangesAsync(cancellationToken);
    }
}
