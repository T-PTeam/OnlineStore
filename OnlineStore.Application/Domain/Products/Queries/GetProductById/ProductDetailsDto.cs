namespace OnlineStore.Application.Domain.Products.Queries.GetProductById;

public record ProductDetailsDto
{
    public long Id { get; init; }

    public string Name { get; init; }

    public string Slug { get; init; }

    public string Description { get; init; }

    public long CategoryId { get; init; }

    public string CategoryName { get; init; }

    public decimal Price { get; init; }

    public string Image { get; init; }
}