namespace OnlineStore.Application.Domain.Products.Commands.RemoveProduct;

public interface IRemoveProductCommand
{
    Task RemoveProduct(long id);
}