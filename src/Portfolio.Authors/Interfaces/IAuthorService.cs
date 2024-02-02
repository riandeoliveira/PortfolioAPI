using Portfolio.Authors.Requests;
using Portfolio.Entities;

namespace Portfolio.Authors.Interfaces;

public interface IAuthorService
{
    Task<Author> CreateAsync(CreateAuthorRequest request);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<Author>> GetAsync();
}
