using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Application.Domain.Products.Commands.CreateProduct;
using OnlineStore.Application.Domain.Products.Commands.RemoveProduct;
using OnlineStore.Application.Domain.Products.Commands.UpdateProduct;
using OnlineStore.Application.Domain.Products.Queries.GetProducts;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly ICreateProductCommand _createProductCommand;

    private readonly IRemoveProductCommand _removeProductCommand;

    private readonly IUpdateProductCommand _updateProductCommand;

    private readonly OnlineStoreDbContext _context;

    private readonly IGetProductQuery _getProductQuery;

    public ProductController(ICreateProductCommand createProductCommand, 
        IRemoveProductCommand removeProductCommand, 
        IUpdateProductCommand updateProductCommand, 
        IWebHostEnvironment webHostEnvironment, 
        OnlineStoreDbContext context, 
        IGetProductQuery getProductQuery)
    {
        _createProductCommand = createProductCommand;
        _removeProductCommand = removeProductCommand;
        _updateProductCommand = updateProductCommand;
        _webHostEnvironment = webHostEnvironment;
        _context = context;
        _getProductQuery = getProductQuery;
    }

    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        int pageSize = 3;
        ViewBag.PageNumber = pageNumber;
        ViewBag.PageRange = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Products.Count() / pageSize);

        return View(await _getProductQuery.GetProduct(pageSize, pageNumber));
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);

        if (ModelState.IsValid)
        {
            await _createProductCommand.CreateProduct(product.Name, product.Description, product.CategoryId, product.Price);

            TempData["Success"] = "The product has been created!";

            return RedirectToAction("Index");
        }

        return View(product);
    }
}