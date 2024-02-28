using Portfolio.Authors.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Context;
using Portfolio.Utils.Repositories;

namespace Portfolio.Authors.Repositories;

public class AuthorRepository(DatabaseContext context) : BaseRepository<Author>(context), IAuthorRepository
{
}
