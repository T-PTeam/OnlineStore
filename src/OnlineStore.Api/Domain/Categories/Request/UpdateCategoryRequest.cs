namespace OnlineStore.Api.Domain.Categories.Request;

public record UpdateCategoryRequest(long Id, string Name, string Slug);