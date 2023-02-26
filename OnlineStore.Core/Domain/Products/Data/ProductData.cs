namespace OnlineStore.Core.Domain.Products.Data;

public record ProductData(string Name, string Slug, string Description, long CategoryId, decimal Price, string Image);