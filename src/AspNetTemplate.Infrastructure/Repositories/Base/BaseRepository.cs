using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Contexts;

namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>(
    ApplicationDbContext context
) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
}
