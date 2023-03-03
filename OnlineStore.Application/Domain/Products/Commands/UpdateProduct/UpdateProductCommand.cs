using MediatR;

namespace OnlineStore.Application.Domain.Products.Commands.UpdateProduct;

public record UpdateProductCommand(long Id, string Name, string Slug, string Description, long CategoryId, decimal Price, string Image) : IRequest<Unit>;