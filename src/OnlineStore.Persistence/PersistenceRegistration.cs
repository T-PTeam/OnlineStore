using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Persistence;

public static class PersistenceRegistration
{
    private const string ConnectionString = "OnlineStoreDb";

    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionString);
        services.AddDbContext<OnlineStoreDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
    }
}