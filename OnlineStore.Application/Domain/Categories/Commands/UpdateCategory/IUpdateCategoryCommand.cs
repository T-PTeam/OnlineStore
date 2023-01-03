using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Application.Domain.Categories.Commands.UpdateCategory;

public interface IUpdateCategoryCommand
{
    Task UpdateCategory(Category category);
}