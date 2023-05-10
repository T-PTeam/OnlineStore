using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Common;
using OnlineStore.Api.Domain.Categories.Request;
using OnlineStore.Application.Domain.Categories.Commands.CreateCategory;
using OnlineStore.Application.Domain.Categories.Commands.RemoveCategory;
using OnlineStore.Application.Domain.Categories.Commands.UpdateCategory;
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
    public async Task<CategoryDto[]> GetCategoryAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetCategoriesQuery(pageNumber, pageSize);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<long> PostCategoryAsync([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(request.Name, request.Slug);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpPut]
    public async Task PutCategoryAsync([FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(request.Id, request.Name, request.Slug);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task RemoveCategoryAsync([FromBody] RemoveCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveCategoryCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }
}