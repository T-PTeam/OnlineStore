using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Models;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Core.Domain.Products.Data;
using OnlineStore.Core.Domain.Products.Validators;

namespace OnlineStore.Core.Domain.Products.Models;

public class Product : Entity
{
    private Product()
    {

    }

    public Product(string name, 
        string slug, 
        string description, 
        long categoryId, 
        decimal price, 
        string image)
    {
        Name = name;
        Slug = slug;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        Image = image;
    }

    public long Id { get; private set; }

    public string Name { get; set; }

    public string Slug { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; private set; }

    public Category Category { get; set; }

    public decimal Price { get; set; }

    public string Image { get; set; }

    public static async Task<Product> CreateAsync(
        string name, 
        string slug, 
        string description, 
        long categoryId, 
        decimal price, 
        string image,
        IProductPriceMustBePositiveChecker productPriceMustBePositiveChecker,
        CancellationToken cancellationToken = default)
    {
        var product = new Product(name, slug, description, categoryId, price, image);
        await ValidateAsync(new CreateProductDataValidator(null, productPriceMustBePositiveChecker), product, cancellationToken);
        return product;
    }

    public void Update(ProductData product)
    {
        Name = product.Name;
        Slug = product.Slug;
        Description = product.Description;
        Price = product.Price;
        CategoryId = product.CategoryId;
        Image = product.Image;
    }
}
