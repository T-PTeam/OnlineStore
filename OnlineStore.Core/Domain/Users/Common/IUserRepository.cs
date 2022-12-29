using OnlineStore.Core.Domain.Users.Models;

namespace OnlineStore.Core.Domain.Users.Common;

public interface IUserRepository
{
    User Find(long id);

    void Add(User user);

    void Delete(long id);

    //void Update(User user);
}
