using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Entities;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class CreateAuthorHandler(IAuthorRepository repository) : IRequestHandler<CreateAuthorRequest, Author>
{
    private readonly IAuthorRepository _repository = repository;

    public async Task<Author> Handle(CreateAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            Name = request.Name,
            FullName = request.FullName,
            Position = request.Position,
            Description = request.Description,
            AvatarUrl = request.AvatarUrl,
            SpotifyAccountName = request.SpotifyAccountName
        };

        var createdAuthor = await _repository.CreateAsync(author);

        await _repository.SaveChangesAsync();

        return createdAuthor;
    }
}
