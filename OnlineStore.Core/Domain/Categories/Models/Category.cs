using OnlineStore.Core.Domain.Categories.Data;
using OnlineStore.Core.Domain.Products.Models;

namespace OnlineStore.Core.Domain.Categories.Models;

public class Category
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

    public static Category Create(string name, string slug)
    {
        return new Category(name, slug);
    }

    public void Update(CategoryData category)
    {
        Name = category.Name;
        Slug = category.Slug;
    }
}