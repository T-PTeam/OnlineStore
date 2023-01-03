using Microsoft.EntityFrameworkCore;
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

    public async Task<Product> Find(long id)
    {
        var product = await _onlineStoreDbContext.Products.SingleOrDefaultAsync(x => x.Id == id);

        return product ?? throw new InvalidOperationException();
    }

    public async Task Add(Product product)
    {
        await _onlineStoreDbContext.Products.AddAsync(product);
    }

    public async Task Delete(long id)
    {
        var productToBeRemoved = await _onlineStoreDbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
        if (productToBeRemoved is null) throw new InvalidOperationException();
        _onlineStoreDbContext.Products.Remove(productToBeRemoved);
    }
}