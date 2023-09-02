using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Products.Common;

public class ProductNameMustBeInputChecker : IProductNameMustBeInputChecker
{
    private readonly OnlineStoreDbContext _dbContext;

    public ProductNameMustBeInputChecker(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsInput(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .AllAsync(product => product.Name != string.Empty, cancellationToken);
    }
}