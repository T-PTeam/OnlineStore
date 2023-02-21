using Microsoft.AspNetCore.Mvc;
using OnlineStore.Web.Clients;

namespace OnlineStore.Web.Infrastructure.Components;

public class CategoriesViewComponent : ViewComponent
{
    private readonly IOnlineStoreClient _client;

    public CategoriesViewComponent(IOnlineStoreClient client)
    {
        _client = client;
    }

    public async Task<IViewComponentResult> InvokeAsync() =>
        View(await _client.CategoriesAllAsync(1, Int32.MaxValue));
}
