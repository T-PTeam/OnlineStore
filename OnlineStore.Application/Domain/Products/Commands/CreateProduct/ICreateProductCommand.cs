namespace OnlineStore.Application.Domain.Products.Commands.CreateProduct;

public interface ICreateProductCommand
{
   Task<long> CreateProduct(string name, string description, long categoryId, decimal price);
}