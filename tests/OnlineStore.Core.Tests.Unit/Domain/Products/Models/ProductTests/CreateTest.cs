using FluentAssertions;
using Moq;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Core.Exceptions;

namespace OnlineStore.Core.Tests.Unit.Domain.Products.Models.ProductTests;

public class CreateTest
{
    private IProductPriceMustBePositiveChecker ProductPriceMustBePositiveChecker { get; }

    public CreateTest()
    {
        ProductPriceMustBePositiveChecker = Mock.Of<IProductPriceMustBePositiveChecker>();
    }

    [Fact]
    public async Task Should_create_product()
    {
        //Arrange
        Mock.Get(ProductPriceMustBePositiveChecker)
            .Setup(x => x.IsPositiveAsync(It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var price = 777.77m;

        //Act
        var product = await Product.CreateAsync("Test", "test", "Decription", 1, price, "image.png", ProductPriceMustBePositiveChecker, CancellationToken.None);

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

        //act
        var action = async () => await Product.CreateAsync("Test", "test", "Decription", 1, price, "image.png", ProductPriceMustBePositiveChecker, CancellationToken.None);


        //assert
        var validationException = action.Should()
            .ThrowAsync<ValidationException>()
            .WithMessage("Validation is failed.")
            .Result.Subject.Single();

        var failure = validationException.Failures.Single();
        failure.PropertyName.Should().Be(nameof(Product.Price));
        failure.ErrorMessage.Should().Be($"Product price: '{price}' must be > 0.");
    }
}