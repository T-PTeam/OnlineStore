using MediatR;
using Microsoft.AspNetCore.Hosting;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Application.Domain.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductPriceMustBePositiveChecker _productPriceMustBePositiveChecker;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CreateProductCommandHandler(
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

    public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        string uploadsDis = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
        string imageName = Guid.NewGuid().ToString() + "_" + command.Image.FileName;

        string filePath = Path.Combine(uploadsDis, imageName);

        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await command.Image.CopyToAsync(fileStream, cancellationToken);
        fileStream.Close();

        var product = await Product.CreateAsync(command.Name, command.Slug, command.Description, command.CategoryId, command.Price, imageName, _productPriceMustBePositiveChecker, cancellationToken);
        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}
