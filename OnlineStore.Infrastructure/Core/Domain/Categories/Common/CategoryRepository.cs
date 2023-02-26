using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Categories.Models;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Categories.Common;

public class CategoryRepository : ICategoryRepository
{
    private readonly OnlineStoreDbContext _onlineStoreDbContext;

    public CategoryRepository(OnlineStoreDbContext onlineStoreDbContext)
    {
        _onlineStoreDbContext = onlineStoreDbContext;
    }

    public async Task<Category> FindAsync(long id)
    {
        var category = await _onlineStoreDbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);

        return category ?? throw new InvalidOperationException();
    }

    public async Task DeleteAsync(long id)
    {
        var categoryToBeRemoved = await _onlineStoreDbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
        if(categoryToBeRemoved is null) throw new InvalidOperationException();
        _onlineStoreDbContext.Categories.Remove(categoryToBeRemoved);
    }

    public async Task AddAsync(Category category)
    {
        await _onlineStoreDbContext.Categories.AddAsync(category);
    }
}