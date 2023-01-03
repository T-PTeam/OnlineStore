using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Application.Domain.Products.Commands.UpdateProduct;

public interface IUpdateProductCommand
{
    Task UpdateProduct(Product product);
}