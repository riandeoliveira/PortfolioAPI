using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.SeedWork;

public sealed class UnitOrWork(DatabaseContext databaseContext) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
