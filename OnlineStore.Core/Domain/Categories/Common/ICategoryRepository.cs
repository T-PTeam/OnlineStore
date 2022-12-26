using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Core.Domain.Categories.Common;

public interface ICategoryRepository
{
    Category Find(long id);

    void Delete(long id);

    void Add(Category category);
}