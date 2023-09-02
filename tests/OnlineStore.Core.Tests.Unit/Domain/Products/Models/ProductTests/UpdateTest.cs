using FluentAssertions;
using Moq;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Data;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Core.Exceptions;

namespace OnlineStore.Core.Tests.Unit.Domain.Products.Models.ProductTests;

public class UpdateTest
{
    private IProductPriceMustBePositiveChecker ProductPriceMustBePositiveChecker { get; }

    public UpdateTest()
    {
        ProductPriceMustBePositiveChecker = Mock.Of<IProductPriceMustBePositiveChecker>();
    }

    [Fact]
    public async Task Should_update_product()
    {
        //Arrange
        Mock.Get(ProductPriceMustBePositiveChecker)
            .Setup(x => x.IsPositiveAsync(It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var product = new Product("Pen", "pen", "description", 1, 777.00m, "image.png");
        var data = new ProductDataUpdate("IPhone 14 Pro", "iphone14pro", "Description", 2, 666.00m, "iphone14pro.png");
        
        //Act
        await product.UpdateAsync(ProductPriceMustBePositiveChecker, data, CancellationToken.None);

        //Assert
        product.Should().NotBeNull();
        product.Price.Should().Be(data.Price);
    }

    [Fact]
    public async Task When_price_is_not_positive()
    {
        //Arrange
        Mock.Get(ProductPriceMustBePositiveChecker)
            .Setup(x => x.IsPositiveAsync(It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var product = new Product("Pen", "pen", "description", 1, 777.00m, "image.png");
        var data = new ProductDataUpdate("IPhone 14 Pro", "iphone14pro", "Description", 2, 666.00m, "iphone14pro.png");

        //Act
        var action = async () => await product.UpdateAsync(ProductPriceMustBePositiveChecker, data, CancellationToken.None);

        //Assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(ProductDataUpdate.Price));
        failure.ErrorMessage.Should().Be($"Product price: '{data.Price}' must be > 0.");
    }

    [Theory]
    [InlineData(100.45, 777.77)]
    [InlineData(4549.99, 10684.00)]
    [InlineData(100000.00, 666.666)]
    public async Task ManyUpdateExample(decimal price, decimal updatePrice)
    {
        //Arrange
        Mock.Get(ProductPriceMustBePositiveChecker)
            .Setup(x => x.IsPositiveAsync(It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var product = new Product("Pen", "pen", "description", 1, price, "image.png");
        var data = new ProductDataUpdate("IPhone 14 Pro", "iphone14pro", "Description", 2, updatePrice, "iphone14pro.png");

        //Act
        var action = async () => await product.UpdateAsync(ProductPriceMustBePositiveChecker, data, CancellationToken.None);

        //Assert
        await product.UpdateAsync(ProductPriceMustBePositiveChecker, data, CancellationToken.None);

        //Assert
        product.Should().NotBeNull();
        product.Price.Should().Be(data.Price);
    }
}