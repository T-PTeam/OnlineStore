namespace OnlineStore.Application.Domain.Products.Queries.GetProducts;

public interface IGetProductQuery
{
    Task<ProductDto[]> GetProduct(int pageSize, int pageNumber);
}