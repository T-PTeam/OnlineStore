namespace OnlineStore.Application.Domain.Categories.Commands.RemoveCategory;

public interface IRemoveCategoryCommand
{
    Task RemoveCategory(long id);
}