using OnlineStore.Core.Domain.Users.Common;
using OnlineStore.Core.Domain.Users.Models;
using OnlineStore.Persistence.OnlineStoreDb;

namespace OnlineStore.Infrastructure.Core.Domain.Users.Common;

public class UserRepository : IUserRepository
{
    private readonly OnlineStoreDbContext _onlineStoreDbContext;

    public UserRepository(OnlineStoreDbContext onlineStoreDbContext)
    {
        _onlineStoreDbContext = onlineStoreDbContext;
    }

    public User Find(long id)
    {
        var user = _onlineStoreDbContext.Users.SingleOrDefault(x => x.Id == id);

        return user ?? throw new InvalidOperationException();
    }

    public void Add(User user)
    {
        _onlineStoreDbContext.Users.Add(user);
    }

    public void Delete(long id)
    {
        var userToBeRemoved = _onlineStoreDbContext.Users.SingleOrDefault(x => x.Id == id);
        if (userToBeRemoved is null) throw new InvalidOperationException();
        _onlineStoreDbContext.Users.Remove(userToBeRemoved);
    }

    //TODO UpdateCommand
    //public Task Update(User user)
    //{
    //    var userUpdate = _onlineStoreDbContext.Users.SingleOrDefault(x => x.Id == user.Id);
    //    if(userUpdate is null) throw new InvalidOperationException();
    //    _onlineStoreDbContext.Users.Update(userUpdate);
    //}
}
