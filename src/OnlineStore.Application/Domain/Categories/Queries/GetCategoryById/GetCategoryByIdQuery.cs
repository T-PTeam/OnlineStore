using MediatR;

namespace OnlineStore.Application.Domain.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(long Id) : IRequest<CategoryDetailsDto>;