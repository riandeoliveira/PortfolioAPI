using AspNetTemplate.Contexts;
using AspNetTemplate.Interfaces;

namespace AspNetTemplate.SeedWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
