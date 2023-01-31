using MediatR;

namespace OnlineStore.Application.Domain.Categories.Queries.GetCategories;

public record GetCategoriesQuery(int PageNumber, int PageSize) : IRequest<CategoryDto[]>;