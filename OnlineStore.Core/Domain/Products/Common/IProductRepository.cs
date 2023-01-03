using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Core.Domain.Products.Common;

public interface IProductRepository
{
    Task<Product> Find(long id);

    Task Add(Product product);

    Task Delete(long id);
}