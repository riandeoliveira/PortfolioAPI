using AspNetTemplate.Infra.Data.Contexts;
using AspNetTemplate.Infra.Data.Interfaces;

namespace AspNetTemplate.Infra.Data.SeedWork;

public sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
