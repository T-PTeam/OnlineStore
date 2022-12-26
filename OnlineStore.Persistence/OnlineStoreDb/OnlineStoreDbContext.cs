using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Domain.Categories.Models;
using OnlineStore.Core.Domain.Products.Models;
using OnlineStore.Core.Domain.Users.Models;

namespace OnlineStore.Persistence.OnlineStoreDb;

public class OnlineStoreDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = @"Server=DESKTOP-K9TMIP8;Database=OnlineStore;Integrated Security=true";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}