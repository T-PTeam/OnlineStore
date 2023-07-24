using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Categories.Common;

public class CategoryNameMustBeInputChecker : ICategoryNameMustBeInputChecker
{
    private readonly OnlineStoreDbContext _dbContext;

    public CategoryNameMustBeInputChecker(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsInput(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .AllAsync(category => category.Name != string.Empty, cancellationToken);
    }
}