using OnlineStore.Core.Domain.Categories.Models;

namespace OnlineStore.Core.Domain.Products.Models;

public class Product
{
    private Product()
    {

    }

    private Product(string name, string description, long categoryId, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }

    public long Id { get; private set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public long CategoryId { get; private set; }

    public Category Category { get; set; }

    public decimal Price { get; set; }

    public static Product Create(string name, string description, long categoryId, decimal price)
    {
        return new Product(name, description, categoryId, price);
    }
}