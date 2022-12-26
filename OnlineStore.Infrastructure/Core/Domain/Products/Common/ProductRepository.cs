using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Products.Common;

public class ProductRepository : IProductRepository
{
    private readonly OnlineStoreDbContext _onlineStoreDbContext;

    public ProductRepository(OnlineStoreDbContext onlineStoreDbContext)
    {
        _onlineStoreDbContext = onlineStoreDbContext;
    }

    public Product Find(long id)
    {
        var product = _onlineStoreDbContext.Products.SingleOrDefault(x => x.Id == id);

        return product ?? throw new InvalidOperationException();
    }

    public void Add(Product product)
    {
        _onlineStoreDbContext.Products.Add(product);
    }

    public void Delete(long id)
    {
        var productToBeRemoved = _onlineStoreDbContext.Products.SingleOrDefault(x => x.Id == id);
        if (productToBeRemoved is null) throw new InvalidOperationException();
        _onlineStoreDbContext.Products.Remove(productToBeRemoved);
    }
}