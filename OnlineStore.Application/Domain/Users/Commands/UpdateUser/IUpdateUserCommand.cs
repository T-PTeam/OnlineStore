using OnlineStore.Core.Domain.Users.Models;

namespace OnlineStore.Application.Domain.Users.Commands.UpdateUser;

public interface IUpdateUserCommand
{
    Task UpdateUser(User user);
}
