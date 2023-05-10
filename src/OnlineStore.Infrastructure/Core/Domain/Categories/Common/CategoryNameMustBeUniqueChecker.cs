using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Categories.Common;

public class CategoryNameMustBeUniqueChecker : ICategoryNameMustBeUniqueChecker
{
    private readonly OnlineStoreDbContext _dbContext;

    public CategoryNameMustBeUniqueChecker(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUnique(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .AllAsync(category => category.Name != name, cancellationToken);
    }
}