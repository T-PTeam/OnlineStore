using OnlineStore.Core.Common;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnlineStoreDbContext _onlineStoreDbContext;

    public UnitOfWork(OnlineStoreDbContext onlineStoreDbContext)
    {
        _onlineStoreDbContext = onlineStoreDbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
      await _onlineStoreDbContext.SaveChangesAsync(cancellationToken);
    }
}