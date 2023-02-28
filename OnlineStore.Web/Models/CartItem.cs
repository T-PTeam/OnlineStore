using OnlineStore.Web.Clients;

namespace OnlineStore.Web.Models;

public class CartItem
{
    public CartItem()
    {
        
    }

    public CartItem(ProductDetailsDto productDto)
    {
        ProductId = productDto.Id;
        ProductName = productDto.Name;
        Price = (decimal)productDto.Price;
        Quantity = 1;
    }

    public long ProductId { get; set; }

    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

}