using Microsoft.AspNetCore.Mvc;
using OnlineStore.Web.Clients;

namespace OnlineStore.Web.Controllers.Domain.Products;

public class ProductsController : Controller
{
    private readonly IOnlineStoreClient _onlineStoreClient;

    public ProductsController(IOnlineStoreClient onlineStoreClient)
    {
        _onlineStoreClient = onlineStoreClient;
    }


    public async Task<IActionResult> Index(
        CancellationToken cancellationToken,
        int p = 1,
        int pageSize = 3,
        string categorySlug = "")
    {
        ViewBag.PageNumber = p;
        ViewBag.PageRange = pageSize;
        ViewBag.CategorySlug = categorySlug;

        return View(await _onlineStoreClient.GetProductAsync(p, pageSize, categorySlug, cancellationToken));
    }
}
