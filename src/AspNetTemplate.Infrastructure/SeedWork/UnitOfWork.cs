using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Contexts;

namespace AspNetTemplate.Infrastructure.SeedWork;

public sealed class UnitOfWork(DatabaseContext databaseContext) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
