namespace OnlineStore.Application.Domain.Categories.Commands.CreateCategory;

public interface ICreateCategoryCommand
{
    Task<long> CreateCategory(string name);
}