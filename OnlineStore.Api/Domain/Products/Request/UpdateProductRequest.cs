namespace OnlineStore.Api.Domain.Products.Request;

public record UpdateProductRequest(long Id,string Name, string Slug, string Description, long CategoryId, decimal Price, string Image);