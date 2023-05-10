using MediatR;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Data;

namespace OnlineStore.Application.Domain.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductPriceMustBePositiveChecker _productPriceMustBePositiveChecker;

    public UpdateProductCommandHandler(
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork, 
        IProductPriceMustBePositiveChecker productPriceMustBePositiveChecker)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _productPriceMustBePositiveChecker = productPriceMustBePositiveChecker;
    }

    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var original =await _productRepository.FindAsync(command.Id);
        var data = new ProductData(command.Name, command.Slug, command.Description, command.CategoryId, command.Price, command.Image);
        await original.UpdateAsync(data, _productPriceMustBePositiveChecker, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}