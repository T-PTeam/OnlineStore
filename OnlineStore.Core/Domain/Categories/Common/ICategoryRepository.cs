using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Core.Domain.Categories.Common;

public interface ICategoryRepository
{
    Task<Category> Find(long id);

    Task Delete(long id);

    Task Add(Category category);
}