using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Application.Domain.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IUpdateProductCommand
{
    private readonly IProductRepository _productRepository;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommand(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateProduct(Product product)
    {
        var original = await _productRepository.Find(product.Id);

        original.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }
}