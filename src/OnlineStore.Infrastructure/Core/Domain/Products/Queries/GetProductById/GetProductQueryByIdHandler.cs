using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Domain.Products.Queries.GetProductById;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Products.Queries.GetProductById;

public class GetProductQueryByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDetailsDto>
{
    private readonly OnlineStoreDbContext _dbContext;

    public GetProductQueryByIdHandler(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDetailsDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _dbContext.Products.AsNoTracking();
        var data = await sqlQuery
            .OrderByDescending(product => product.Id)
            .Include(product => product.Category)
            .Select(product => new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                Price = product.Price,
                Image = product.Image
            })
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        return data;
    }
}