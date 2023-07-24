using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Common;
using OnlineStore.Api.Common.Responses;
using OnlineStore.Api.Domain.Products.Request;
using OnlineStore.Application.Domain.Products.Commands.CreateProduct;
using OnlineStore.Application.Domain.Products.Commands.RemoveProduct;
using OnlineStore.Application.Domain.Products.Commands.UpdateProduct;
using OnlineStore.Application.Domain.Products.Queries.GetProducts;
using OnlineStore.Application.Domain.Products.Queries.GetProductById;

namespace OnlineStore.Api.Domain.Products;


[ApiController]
[Route(Routs.Products)]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<PaginationResponse<ProductDto[]>> GetProductAsync(int pageNumber, int pageSize, CancellationToken cancellationToken,
        string? categorySlug = "")
    {
        var query = new GetProductQuery(pageNumber, pageSize, categorySlug); 
        var response = await _mediator.Send(query, cancellationToken);
        return new PaginationResponse<ProductDto[]>(response.data, response.total);
    }

    [HttpGet("{id}")]
    public async Task<ProductDetailsDto> GetProductByIdAsync(long id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(id);
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<long> PostProductAsync([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(request.Name, request.Slug, request.Description, request.CategoryId, request.Price, request.Image);
        var id = await _mediator.Send(command, cancellationToken);
        return id;
    }

    [HttpPut]
    public async Task PutProduct([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(request.Id, request.Name, request.Slug, request.Description, request.CategoryId, request.Price, request.Image);
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    public async Task RemoveProductAsync([FromBody] RemoveProductRequest request, CancellationToken cancellationToken)
    {
        var command = new RemoveProductCommand(request.Id);
        await _mediator.Send(command, cancellationToken);
    }
}