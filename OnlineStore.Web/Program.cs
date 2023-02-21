using OnlineStore.Application;
using OnlineStore.Infrastructure;
using OnlineStore.Persistence;
using OnlineStore.Web;
using OnlineStore.Web.Clients;
using OnlineStore.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructureRegistration();
builder.Services.AddApplicationRegistration();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddHttpClient<IOnlineStoreClient, OnlineStoreClient>((HttpClient httpClient, IServiceProvider provider) =>
{
    return new OnlineStoreClient("https://localhost:7023/", httpClient);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
