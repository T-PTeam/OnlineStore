using MediatR;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Application.Domain.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.Create(command.Name, command.Slug, command.Description, command.CategoryId, command.Price, command.Image);
        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}