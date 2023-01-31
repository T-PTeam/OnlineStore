using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.Categories.Models;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Persistence.OnlineStoreDb.EntityConfigurations;

namespace OnlineStore.Persistence.OnlineStoreDb;

public class OnlineStoreDbContext : DbContext
{
    public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityConfigurations());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfigurations());
    }
}