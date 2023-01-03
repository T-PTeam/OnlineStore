using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Application.Domain.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : ICreateCategoryCommand
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommand(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> CreateCategory(string name)
    {
        var category = Category.Create(name);
        await _categoryRepository.Add(category);
        await _unitOfWork.SaveChangesAsync();
        return category.Id;
    }
}