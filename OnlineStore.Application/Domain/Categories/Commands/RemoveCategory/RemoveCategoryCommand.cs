using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;

namespace OnlineStore.Application.Domain.Categories.Commands.RemoveCategory;

public class RemoveCategoryCommand : IRemoveCategoryCommand
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public RemoveCategoryCommand(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task RemoveCategory(long id)
    {
        await _categoryRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }
}