namespace OnlineStore.Application.Domain.Users.Commands.RemoveUser;

public interface IRemoveUserCommand
{
    Task RemoveUser(long id);
}