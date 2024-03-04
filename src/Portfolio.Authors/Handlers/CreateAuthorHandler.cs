using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Messaging;

namespace Portfolio.Authors.Handlers;

public sealed class CreateAuthorHandler(IAuthorRepository authorRepository) : IRequestHandler<CreateAuthorRequest, Author>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<Author> Handle(CreateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        Author author = new()
        {
            Name = request.Name,
            FullName = request.FullName,
            Position = request.Position,
            Description = request.Description,
            AvatarUrl = request.AvatarUrl,
            SpotifyAccountName = request.SpotifyAccountName
        };

        Author createdAuthor = await _authorRepository.CreateAsync(author, cancellationToken);

        await _authorRepository.SaveChangesAsync(cancellationToken);

        return createdAuthor;
    }
}
