namespace OnlineStore.Application.Domain.Categories.Commands.CreateCategory;

public interface ICreateCategoryCommand
{
    long CreateCategory(string name);
}