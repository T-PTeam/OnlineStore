using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.IdentityServer.Persistence.IdentityDb;

public class IdentityDbContext : IdentityDbContext<IdentityUser<long>, IdentityRole<long>, long>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}