using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Domain.Categories.Queries.GetCategories;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Categories.Queries.GetCategories;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoriesQuery, CategoryDto[]>
{
    private readonly OnlineStoreDbContext _dbContext;

    public GetCategoryQueryHandler(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryDto[]> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _dbContext.Categories.AsNoTracking();
        var skip = (request.PageNumber - 1) * request.PageSize;
        var data = await sqlQuery
            .OrderBy(category => category.Id)
            .Include(x => x.Products)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                ProductsCollection = x.Products.Select(product => new ProductsDto()
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Description = product.Description,
                    CategoryName = product.Category.Name,
                    Price = product.Price,
                    Image = product.Image
                }).ToList()
            }).ToArrayAsync(cancellationToken);

        return data;
    }
}