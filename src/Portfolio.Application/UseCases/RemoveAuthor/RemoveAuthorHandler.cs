using MediatR;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.RemoveAuthor;

public sealed class RemoveAuthorHandler(
    IAuthorRepository authorRepository,
    RemoveAuthorValidator validator
) : IRequestHandler<RemoveAuthorRequest, RemoveAuthorResponse>
{
    public async Task<RemoveAuthorResponse> Handle(RemoveAuthorRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateOrThrowAsync(request, cancellationToken);

        Author author = await authorRepository.FindOneOrThrowAsync(request.Id, cancellationToken);

        await authorRepository.RemoveSoftAsync(author, cancellationToken);
        await authorRepository.SaveChangesAsync(cancellationToken);

        return new RemoveAuthorResponse();
    }
}
