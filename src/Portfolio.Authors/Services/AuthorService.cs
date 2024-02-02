using Portfolio.Authors.Interfaces;
using Portfolio.Authors.Requests;
using Portfolio.Entities;

namespace Portfolio.Authors.Services;

public sealed class AuthorService(IAuthorRepository repository) : IAuthorService
{
    private readonly IAuthorRepository _repository = repository;

    public async Task<Author> CreateAsync(CreateAuthorRequest request)
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

    public async Task DeleteAsync(Guid id)
    {
        var author = await _repository.GetAsync();
        var authorToDelete = author.Where(x => x.Id == id).First();

        await _repository.DeleteAsync(authorToDelete);
        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Author>> GetAsync()
    {
        return await _repository.GetAsync();
    }
}
