using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Web.Areas.Admin.ViewsModels;
using OnlineStore.Web.Clients;
using System.Reflection;

namespace OnlineStore.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IOnlineStoreClient _client;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductsController(IOnlineStoreClient client, IWebHostEnvironment webHostEnvironment)
    {
        _client = client;
        _webHostEnvironment = webHostEnvironment;
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
    ProductViewModel model,
    CancellationToken cancellationToken)
    {
        if (model.ImageUpload != null)
        {
            string uploadsDis = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
            string imageName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;

            string filePath = Path.Combine(uploadsDis, imageName);

            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await model.ImageUpload.CopyToAsync(fileStream, cancellationToken);
            fileStream.Close();

            request.Image = imageName;
        }


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
        ProductViewModel model,
        CancellationToken cancellationToken)
    {
        if (model.ImageUpload != null)
        {
            string uploadsDis = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
            string imageName = Guid.NewGuid().ToString() + "_" + model.ImageUpload.FileName;

            string filePath = Path.Combine(uploadsDis, imageName);

            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            await model.ImageUpload.CopyToAsync(fileStream, cancellationToken);
            fileStream.Close();

            request.Image = imageName;
        }

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