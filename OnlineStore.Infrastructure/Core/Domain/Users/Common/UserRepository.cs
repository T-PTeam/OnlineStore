using Microsoft.EntityFrameworkCore;
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

    public async Task<User> Find(long id)
    {
        var user = await _onlineStoreDbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

        return user ?? throw new InvalidOperationException();
    }

    public async Task Add(User user)
    { 
        await _onlineStoreDbContext.Users.AddAsync(user);
    }

    public async Task Delete(long id)
    {
        var userToBeRemoved = await _onlineStoreDbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        if (userToBeRemoved is null) throw new InvalidOperationException();
        _onlineStoreDbContext.Users.Remove(userToBeRemoved);
    }
}
