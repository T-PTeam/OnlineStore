namespace OnlineStore.Application.Domain.Categories.Queries.GetCategories;

public record ProductsDto
{
    public long ProductId { get; init; }

    public string Name { get; init; }

    public string Slug { get; init; }

    public string Description { get; init; }

    public string CategoryName { get; init; }

    public decimal Price { get; init; }

    public string Image { get; init; }
}