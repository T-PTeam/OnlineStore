using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Web.Clients;

namespace OnlineStore.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IOnlineStoreClient _client;

    public CategoryController(IOnlineStoreClient client)
    {
        _client = client;
    }

    public async Task<IActionResult> Index(
        CancellationToken cancellationToken,
        int p = 1,
        int pageSize = Int32.MaxValue)
    {
        return View(await _client.GetCategoryAsync(p, pageSize, cancellationToken));
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        await _client.PostCategoryAsync(request, cancellationToken);
        TempData["Success"] = "The category has been created!";

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(long id)
    {
        var category = await _client.GetCategoryByIdAsync(id);

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(
        UpdateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        await _client.PutCategoryAsync(request, cancellationToken);
        TempData["Success"] = "The category has been updated!";

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(
        RemoveCategoryRequest request,
        CancellationToken cancellationToken)
    {
        await _client.RemoveCategoryAsync(request, cancellationToken);
        TempData["Success"] = "The category has been deleted!";

        return RedirectToAction("Index");
    }
}