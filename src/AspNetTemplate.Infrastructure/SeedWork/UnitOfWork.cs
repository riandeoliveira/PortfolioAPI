using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Contexts;

namespace AspNetTemplate.Infrastructure.SeedWork;

public sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
