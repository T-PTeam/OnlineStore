namespace OnlineStore.Core.Domain.Categories.Models;

public class Category
{
    private Category()
    {

    }

    private Category(string name)
    {
        Name = name;
    }

    public long Id { get; private set; }

    public string Name { get; set; }

    public static Category Create(string name)
    {
        return new Category(name);
    }

    public void Update(Category category)
    {
        Name = category.Name;
    }
}