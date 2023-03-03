using MediatR;

namespace OnlineStore.Application.Domain.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(long Id, string Name, string Slug) : IRequest<Unit>;