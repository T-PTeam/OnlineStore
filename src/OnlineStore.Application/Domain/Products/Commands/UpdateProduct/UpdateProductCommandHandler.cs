using MediatR;
using Microsoft.AspNetCore.Hosting;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Data;

namespace OnlineStore.Application.Domain.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductPriceMustBePositiveChecker _productPriceMustBePositiveChecker;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UpdateProductCommandHandler(
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork, 
        IProductPriceMustBePositiveChecker productPriceMustBePositiveChecker, 
        IWebHostEnvironment webHostEnvironment)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _productPriceMustBePositiveChecker = productPriceMustBePositiveChecker;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        string uploadsDis = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
        string imageName = Guid.NewGuid().ToString() + "_" + command.Image.FileName;

        string filePath = Path.Combine(uploadsDis, imageName);

        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await command.Image.CopyToAsync(fileStream, cancellationToken);
        fileStream.Close();


        var original = await _productRepository.FindAsync(command.Id);
        var data = new ProductData(command.Name, command.Slug, command.Description, command.CategoryId, command.Price, imageName);
        await original.UpdateAsync(data, _productPriceMustBePositiveChecker, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}