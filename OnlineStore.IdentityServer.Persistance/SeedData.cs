using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.IdentityServer.Persistence.IdentityDb;

namespace OnlineStore.IdentityServer.Persistence;

public class SeedData
{
    public static void SeedDataBase(IdentityDbContext context)
    {
        context.Database.Migrate();

        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new IdentityRole<long>
                {
                    Name = "Admin",
                    NormalizedName = "admin"
                },
                new IdentityRole<long>
                {
                    Name = "User",
                    NormalizedName = "user",
                }
            );

            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            context.Users.AddRange(
            new IdentityUser<long>
            {
                
            });
        }
    }
}