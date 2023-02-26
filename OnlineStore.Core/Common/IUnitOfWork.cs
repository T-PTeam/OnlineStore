namespace OnlineStore.Core.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellation);
}