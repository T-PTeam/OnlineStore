using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Core.Domain.Categories.Common;

public interface ICategoryRepository
{
    Task<Category> FindAsync(long id);

    Task DeleteAsync(long id);

    Task AddAsync(Category category);
}