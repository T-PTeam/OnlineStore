namespace OnlineStore.Api.Domain.Products.Request;

public record CreateProductRequest(string Name, string Slug, string Description, long CategoryId, decimal Price, IFormFile Image);