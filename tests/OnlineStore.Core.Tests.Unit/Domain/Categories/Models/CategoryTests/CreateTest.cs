using FluentAssertions;
using Moq;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Core.Tests.Unit.Domain.Categories.Models.CategoryTests;

public class CreateTest
{
    private ICategoryNameMustBeUniqueChecker CategoryNameMustBeUniqueChecker { get; }

    public CreateTest()
    {
        CategoryNameMustBeUniqueChecker = Mock.Of<ICategoryNameMustBeUniqueChecker>();
    }

    [Fact]
    public async Task Should_create_category()
    {
        //Arrange
        Mock.Get(CategoryNameMustBeUniqueChecker)
            .Setup(x => x.IsUnique(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var name = "Test";

        //Act
        var category = await Category.CreateAsync(name, "test", CategoryNameMustBeUniqueChecker);

        //Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(name);
    }
}