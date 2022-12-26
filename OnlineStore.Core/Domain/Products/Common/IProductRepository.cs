using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Core.Domain.Products.Common;

public interface IProductRepository
{
    Product Find(long id);

    void Add(Product product);

    void Delete(long id);
}