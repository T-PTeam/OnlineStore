using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Persistence.OnlineStoreDb;

public class SeedData
{
    public static void SeedDataBase(OnlineStoreDbContext context)
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
    }
}