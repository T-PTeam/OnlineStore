using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Core.Domain.Products.Common;

public interface IProductRepository
{
    Task<Product> FindAsync(long id);

    Task AddAsync(Product product);

    Task DeleteAsync(long id);
}