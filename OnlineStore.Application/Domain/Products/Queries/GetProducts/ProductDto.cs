namespace OnlineStore.Application.Domain.Products.Queries.GetProducts;

public record ProductDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; set; }

    public decimal Price { get; set; }
}