namespace OnlineStore.Application.Domain.Users.Commands.CreateUser;

public interface ICreateUserCommand
{
    Task<long> CreateUser(string nickName, string numberPhone, string email, string password);
}