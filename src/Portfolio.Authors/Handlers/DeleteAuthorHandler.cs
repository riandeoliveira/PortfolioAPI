using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class DeleteAuthorHandler(IAuthorRepository repository) : IRequestHandler<DeleteAuthorRequest>
{
    private readonly IAuthorRepository _repository = repository;

    public async Task Handle(DeleteAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetAsync();
        var authorToDelete = author.Where(x => x.Id == request.Id).First();

        await _repository.DeleteAsync(authorToDelete);
        await _repository.SaveChangesAsync();
    }
}
