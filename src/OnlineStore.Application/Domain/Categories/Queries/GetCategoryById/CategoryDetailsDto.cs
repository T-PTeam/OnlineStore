namespace OnlineStore.Application.Domain.Categories.Queries.GetCategoryById;

public record CategoryDetailsDto
{
    public long Id { get; init; }

    public string Name { get; init; }

    public string Slug { get; init; }
}