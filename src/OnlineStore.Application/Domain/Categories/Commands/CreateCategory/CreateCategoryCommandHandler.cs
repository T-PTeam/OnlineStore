using MediatR;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Application.Domain.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, long>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryNameMustBeUniqueChecker _categoryNameMustBeUniqueChecker;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository, 
        IUnitOfWork unitOfWork, 
        ICategoryNameMustBeUniqueChecker categoryNameMustBeUniqueChecker)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _categoryNameMustBeUniqueChecker = categoryNameMustBeUniqueChecker;
    }

    public async Task<long> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await Category.CreateAsync(request.Name, request.Slug, _categoryNameMustBeUniqueChecker, cancellationToken);
        await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return category.Id;
    }
}