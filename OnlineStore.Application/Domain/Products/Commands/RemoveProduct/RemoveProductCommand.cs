using MediatR;

namespace OnlineStore.Application.Domain.Products.Commands.RemoveProduct;

public record RemoveProductCommand(long Id) : IRequest<Unit>;