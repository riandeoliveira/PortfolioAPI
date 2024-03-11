using Portfolio.Authors.Interfaces;
using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Authors.Repositories;

public class AuthorRepository(
    DatabaseContext databaseContext,
    ILocalizationService localizationService
) : BaseRepository<Author>(databaseContext, localizationService), IAuthorRepository
{
}
