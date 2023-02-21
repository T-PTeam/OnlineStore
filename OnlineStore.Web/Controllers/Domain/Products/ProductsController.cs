using Microsoft.AspNetCore.Mvc;
using OnlineStore.Web.Clients;

namespace OnlineStore.Web.Controllers.Domain.Products;

public class ProductsController : Controller
{
    private readonly IOnlineStoreClient _onlineStoreClient;
    private readonly IEnumerable<ProductsDto> _products;

    public ProductsController(IOnlineStoreClient onlineStoreClient, IEnumerable<ProductsDto> products)
    {
        _onlineStoreClient = onlineStoreClient;
        _products = products;
    }


    //TODO: Total pages code
    public async Task<IActionResult> Index(
        CancellationToken cancellationToken,
        int p = 1,
        int pageSize = int.MaxValue,
        string? categorySlug = "")
    {
        //ViewBag.TotalPages = (int)Math.Ceiling((decimal)_products.Count() / pageSize);
        ViewBag.PageNumber = p;
        ViewBag.PageRange = pageSize;
        ViewBag.CategorySlug = categorySlug;

        return View(await _onlineStoreClient.ProductsGETAsync(p, pageSize, categorySlug, cancellationToken));
    }
}
