using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Domain.Categories.Queries.GetCategoryById;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery,CategoryDetailsDto>
{
    private readonly OnlineStoreDbContext _dbContext;

    public GetCategoryByIdQueryHandler(OnlineStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryDetailsDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var sqlQuery = _dbContext.Categories.AsNoTracking();
        var data = await sqlQuery.OrderByDescending(category => category.Id)
            .Select(category => new CategoryDetailsDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            })
            .FirstOrDefaultAsync(category => category.Id == request.Id, cancellationToken);

        return data;
    }
}