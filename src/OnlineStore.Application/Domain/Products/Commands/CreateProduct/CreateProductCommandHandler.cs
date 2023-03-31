using MediatR;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Application.Domain.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductPriceMustBePositiveChecker _productPriceMustBePositiveChecker;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IProductPriceMustBePositiveChecker productPriceMustBePositiveChecker)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _productPriceMustBePositiveChecker = productPriceMustBePositiveChecker;
    }

    public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await Product.CreateAsync(command.Name, command.Slug, command.Description, command.CategoryId, command.Price, command.Image, _productPriceMustBePositiveChecker, cancellationToken);
        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}