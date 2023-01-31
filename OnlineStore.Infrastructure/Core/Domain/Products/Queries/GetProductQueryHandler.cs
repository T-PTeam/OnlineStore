using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Domain.Products.Queries.GetProducts;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Products.Queries;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto[]>
{
    private readonly OnlineStoreDbContext _dbContext;

    public GetProductQueryHandler(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDto[]> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _dbContext.Products.AsNoTracking();
        var skip = (request.PageNumber - 1) * request.PageSize;
        var data = await sqlQuery
            .OrderByDescending(product => product.Id)
            .Include(product => product.Category)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(product => new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                Price = product.Price,
                Image = product.Image
            }).ToArrayAsync(cancellationToken);

        return data;
    }
}