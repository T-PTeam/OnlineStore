using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;

namespace OnlineStore.Application.Domain.Products.Commands.RemoveProduct;

public class RemoveProductCommand : IRemoveProductCommand
{
    private readonly IProductRepository _productRepository;

    private readonly IUnitOfWork _unitOfWork;

    public RemoveProductCommand(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task RemoveProduct(long id)
    {
        _productRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }
}