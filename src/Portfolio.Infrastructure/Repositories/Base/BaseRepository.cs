using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>(
    DatabaseContext databaseContext
) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
}
