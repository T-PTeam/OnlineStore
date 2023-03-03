using MediatR;

namespace OnlineStore.Application.Domain.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, string Slug, string Description, long CategoryId, decimal Price, string Image) : IRequest<long>;