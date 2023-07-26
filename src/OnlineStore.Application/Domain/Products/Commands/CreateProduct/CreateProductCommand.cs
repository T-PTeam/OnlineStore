using MediatR;
using Microsoft.AspNetCore.Http;

namespace OnlineStore.Application.Domain.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, string Slug, string Description, long CategoryId, decimal Price, IFormFile Image) : IRequest<long>;