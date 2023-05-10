using MediatR;

namespace OnlineStore.Application.Domain.Products.Queries.GetProducts;

public record GetProductQuery(int PageNumber, int PageSize, string? CategorySlug = "") : IRequest<(ProductDto[] data, int total)>;