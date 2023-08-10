using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Domain.Users.Model
{
    public class User : IdentityUser
    {
        public User(string username,string password,string email) 
        {
            Email = email;
            UserName = username;
            PasswordHash = password;
        }

        public static async Task<User> CreateAsync(string username, string password,string email,CancellationToken cancellationToken)
        {
            User user = new User(username, password, email); 
            return user;
        }
    }
}
