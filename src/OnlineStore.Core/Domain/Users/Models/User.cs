using Microsoft.AspNetCore.Identity;

namespace OnlineStore.Core.Domain.Users.Models;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}