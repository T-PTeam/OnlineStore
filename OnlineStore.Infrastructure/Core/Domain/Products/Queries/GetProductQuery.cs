using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Domain.Products.Queries.GetProducts;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Products.Queries;

public class GetProductQuery : IGetProductQuery
{
    private readonly OnlineStoreDbContext _dbContext;

    public GetProductQuery(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDto[]> GetProduct(int pageSize, int pageNumber)
    {
        var sqlQuery = _dbContext.Products.AsNoTracking();
        var skip = (pageNumber - 1) * pageSize;
        var data = sqlQuery
            .OrderByDescending(product => product.Id)
            .Include(product => product.Category)
            .Skip(skip)
            .Take(pageSize)
            .Select(product => new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price
            }).ToArrayAsync();

        return await data;
    }
}