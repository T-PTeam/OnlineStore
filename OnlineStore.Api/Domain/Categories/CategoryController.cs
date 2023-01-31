using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Common;
using OnlineStore.Api.Domain.Categories.Request;
using OnlineStore.Application.Domain.Categories.Commands.CreateCategory;
using OnlineStore.Application.Domain.Categories.Queries.GetCategories;

namespace OnlineStore.Api.Domain.Categories;

[ApiController]
[Route(Routs.Categories)]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<CategoryDto[]> GetCategory(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetCategoriesQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<long> PostCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(request.Name, request.Slug);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }
}