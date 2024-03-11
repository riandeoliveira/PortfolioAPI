using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity>(
    DatabaseContext databaseContext,
    ILocalizationService localizationService
) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
}
