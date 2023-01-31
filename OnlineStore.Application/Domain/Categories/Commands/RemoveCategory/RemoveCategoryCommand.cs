using MediatR;

namespace OnlineStore.Application.Domain.Categories.Commands.RemoveCategory;

public record RemoveCategoryCommand(long Id) : IRequest<Unit>;