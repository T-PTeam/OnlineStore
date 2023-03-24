using MediatR;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Data;

namespace OnlineStore.Application.Domain.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var original =await _productRepository.FindAsync(command.Id);
        var data = new ProductData(command.Name, command.Slug, command.Description, command.CategoryId, command.Price, command.Image);
        original.Update(data);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}