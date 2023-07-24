using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Categories.Data;
using OnlineStore.Core.Domain.Categories.Validators;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Core.Domain.Categories.Models;

public class Category : Entity
{
    private Category()
    {

    }

    private Category(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }

    public long Id { get; private set; }

    public string Name { get; set; }

    public string Slug { get; set; }

    public IReadOnlyCollection<Product> Products { get; private set; }

    public static async Task<Category> CreateAsync(
        string name, 
        string slug,
        ICategoryNameMustBeUniqueChecker categoryNameMustBeUniqueChecker,
        ICategoryNameMustBeInputChecker categoryNameMustBeInputChecker,
        CancellationToken cancellationToken = default)
    {
        var category = new Category(name,slug);
        await ValidateAsync(new CreateCategoryDataValidator(null, categoryNameMustBeUniqueChecker, categoryNameMustBeInputChecker), category, cancellationToken);
        return category;
    }

    public void Update(CategoryData category)
    {
        Name = category.Name;
        Slug = category.Slug;
    }
}