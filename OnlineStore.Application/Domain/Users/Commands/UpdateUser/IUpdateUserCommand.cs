namespace OnlineStore.Application.Domain.Users.Commands.UpdateUser;

public interface IUpdateUserCommand
{
    Task UpdateUser(long id, string nickName, string numberPhone, string email, string password);
}
