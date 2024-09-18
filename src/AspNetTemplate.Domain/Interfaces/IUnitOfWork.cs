namespace AspNetTemplate.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
