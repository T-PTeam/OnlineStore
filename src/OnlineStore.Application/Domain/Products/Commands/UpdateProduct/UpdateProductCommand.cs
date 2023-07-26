using MediatR;
using Microsoft.AspNetCore.Http;

namespace OnlineStore.Application.Domain.Products.Commands.UpdateProduct;

public record UpdateProductCommand(long Id, string Name, string Slug, string Description, long CategoryId, decimal Price, IFormFile Image) : IRequest<Unit>;