using Portfolio.Authors.Interfaces;
using Portfolio.Entities;
using Portfolio.Entities.Context;
using Portfolio.Utils.Repositories;

namespace Portfolio.Authors.Repositories;

public class AuthorRepository(DatabaseContext context) : BaseRepository<Author>(context), IAuthorRepository
{

}
