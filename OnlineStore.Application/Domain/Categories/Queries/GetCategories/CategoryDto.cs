namespace OnlineStore.Application.Domain.Categories.Queries.GetCategories;

public record CategoryDto
{
    public long Id { get; init; }

    public string Name { get; init; }

    public string Slug { get; init; }

    public IReadOnlyCollection<ProductsDto> ProductsCollection { get; init; }
}