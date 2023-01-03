using OnlineStore.Core.Domain.Users.Models;

namespace OnlineStore.Core.Domain.Users.Common;

public interface IUserRepository
{
    Task<User> Find(long id);

    Task Add(User user);

    Task Delete(long id);
}
