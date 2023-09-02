using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Core.Domain.Users.Data
{
    public record UserData(Guid id, string firstName, string lastName, string email, string password);
}
