using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Application.UseCases.RemoveAuthor;

public sealed class RemoveAuthorHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<RemoveAuthorRequest, RemoveAuthorResponse>
{
    public async Task<RemoveAuthorResponse> Handle(RemoveAuthorRequest request, CancellationToken cancellationToken = default)
    {
        Author author = await authorRepository.FindOneOrThrowAsync(request.Id, cancellationToken);

        await authorRepository.RemoveSoftAsync(author, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new RemoveAuthorResponse();
    }
}
