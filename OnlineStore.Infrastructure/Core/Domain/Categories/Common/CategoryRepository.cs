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

    public Category Find(long id)
    {
        var category = _onlineStoreDbContext.Categories.SingleOrDefault(x => x.Id == id);

        return category ?? throw new InvalidOperationException();
    }

    public void Delete(long id)
    {
        var categoryToBeRemoved = _onlineStoreDbContext.Categories.SingleOrDefault(x => x.Id == id);
        if(categoryToBeRemoved is null) throw new InvalidOperationException();
        _onlineStoreDbContext.Categories.Remove(categoryToBeRemoved);
    }

    public void Add(Category category)
    {
        _onlineStoreDbContext.Categories.Add(category);
    }
}