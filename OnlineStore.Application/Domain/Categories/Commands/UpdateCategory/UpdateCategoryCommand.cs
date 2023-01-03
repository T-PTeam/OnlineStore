using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Application.Domain.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IUpdateCategoryCommand
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommand(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateCategory(Category category)
    {
        var original = await _categoryRepository.Find(category.Id);

        original.Update(category);
        await _unitOfWork.SaveChangesAsync();
    }
}