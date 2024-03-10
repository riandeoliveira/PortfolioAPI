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
    public async Task Handle(RemoveAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateRequestAsync(request, cancellationToken);

        Author author = await authorRepository.FindOrThrowAsync(request.Id, cancellationToken);

        await authorRepository.RemoveSoftAsync(author, cancellationToken);
        await authorRepository.SaveChangesAsync(cancellationToken);
    }
}
