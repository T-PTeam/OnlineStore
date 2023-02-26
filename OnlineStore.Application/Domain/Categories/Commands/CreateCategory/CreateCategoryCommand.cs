using MediatR;

namespace OnlineStore.Application.Domain.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Slug) : IRequest<long>;