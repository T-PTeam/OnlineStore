using MediatR;

namespace OnlineStore.Application.Domain.Products.Queries.GetProductById;

public record GetProductByIdQuery(long Id) : IRequest<ProductDetailsDto>;