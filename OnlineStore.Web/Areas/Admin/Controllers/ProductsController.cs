using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Web.Clients;

namespace OnlineStore.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IOnlineStoreClient _client;

    public ProductsController(IOnlineStoreClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> Index(
        CancellationToken cancellationToken,
        int p = 1,
        int pageSize = 5,
        string categorySlug = "")
    {
        ViewBag.PageNumber = p;
        ViewBag.PageRange = pageSize;

        return View(await _client.GetProductAsync(p, pageSize, categorySlug, cancellationToken));
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _client.GetCategoryAsync(1, Int32.MaxValue);
        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View();
    }

    //TODO: Post product
    [HttpPost]
    public async Task<IActionResult> Create(
    CreateProductRequest request,
    CancellationToken cancellationToken)
    {
        //TODO: Create find category by id
        await _client.GetCategoryAsync(1, Int32.MaxValue, cancellationToken); 
        await _client.PostProductAsync(request, cancellationToken);
        TempData["Success"] = "The product has been created!";

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(long id)
    {
       var product = await _client.GetProductByIdAsync(id);
       var categories = await _client.GetCategoryAsync(1, Int32.MaxValue);
       ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        await _client.GetCategoryAsync(1, Int32.MaxValue, cancellationToken);
        await _client.PutProductAsync(request, cancellationToken);
        TempData["Success"] = "The product has been updated!";

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(
    RemoveProductRequest request,
    CancellationToken cancellationToken)
    {
        await _client.RemoveProductAsync(request, cancellationToken);
        TempData["Success"] = "The product has been deleted!";
        return RedirectToAction("Index");
    }
}