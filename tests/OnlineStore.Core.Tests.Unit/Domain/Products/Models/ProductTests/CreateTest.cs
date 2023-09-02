using FluentAssertions;
using Moq;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Data;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Core.Exceptions;

namespace OnlineStore.Core.Tests.Unit.Domain.Products.Models.ProductTests;

public class CreateTest
{
    private IProductPriceMustBePositiveChecker ProductPriceMustBePositiveChecker { get; }

    private IProductNameMustBeInputChecker ProductNameMustBeInputChecker { get; }

    public CreateTest()
    {
        ProductPriceMustBePositiveChecker = Mock.Of<IProductPriceMustBePositiveChecker>();
        ProductNameMustBeInputChecker = Mock.Of<IProductNameMustBeInputChecker>();
    }

    [Fact]
    public async Task Should_create_product()
    {
        //Arrange
        Mock.Get(ProductPriceMustBePositiveChecker)
            .Setup(x => x.IsPositiveAsync(It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        Mock.Get(ProductNameMustBeInputChecker)
            .Setup(x=> x.IsInput(It.IsAny<string>(),It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        
        var price = 777.77m;
        var data = new ProductDataCreate("Test", "test", "Decription", 1, price, "image.png");

        //Act
        var product = await Product.CreateAsync(ProductPriceMustBePositiveChecker, ProductNameMustBeInputChecker, data, CancellationToken.None);

        //Assert 
        product.Should().NotBeNull();
        product.Price.Should().Be(price);
    }

    [Fact]
    public async Task When_price_is_not_positive_Should_throw_exception()
    {
        //Arrange
        Mock.Get(ProductPriceMustBePositiveChecker)
            .Setup(x => x.IsPositiveAsync(It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var price = 777.77m;
        var data = new ProductDataCreate("Test", "test", "Decription", 1, price, "image.png");

        //act
        var action = async () => await Product.CreateAsync(ProductPriceMustBePositiveChecker, ProductNameMustBeInputChecker, data, CancellationToken.None);


        //assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result;

        var failure = validationException.And.Failures.First();
        failure.PropertyName.Should().Be(nameof(Product.Price));
        failure.ErrorMessage.Should().Be($"Product price: '{price}' must be > 0.");
    }

    [Fact]
    public async Task When_name_is_not_input_Should_throw_exception()
    {
        //Arrange
        Mock.Get(ProductNameMustBeInputChecker)
            .Setup(x => x.IsInput(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var name = "Test";
        var data = new ProductDataCreate(name, "test", "Decription", 1, 777.77m, "image.png");

        //act
        var action = async () => await Product.CreateAsync(ProductPriceMustBePositiveChecker, ProductNameMustBeInputChecker, data, CancellationToken.None);

        //assets
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result;

        var failure = validationException.And.Failures.Last();
        failure.PropertyName.Should().Be(nameof(Product.Name));
        failure.ErrorMessage.Should().Be("Product name must be input.");
    }
}