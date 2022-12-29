using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Application.Domain.Products.Commands.CreateProduct;

public class CreateProductCommand : ICreateProductCommand
{
    private readonly IProductRepository _productRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommand(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> CreateProduct(string name, string description, long categoryId, decimal price)
    {
        var product = Product.Create(name, description, categoryId, price);
        _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync();
        return product.Id;
    }
}