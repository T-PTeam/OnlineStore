namespace OnlineStore.Core.Domain.Users.Models;

public class User
{
    private User()
    {

    }

    private User(string nickName, string email, string numberPhone, string password)
    {
        NickName = nickName;
        NumberPhone = numberPhone;
        Email = email;
        Password = password;
    }

    public long Id { get; private set; }

    public string NickName { get; set; }

    public string NumberPhone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public static User Create(string nickName, string numberPhone, string email, string password)
    {
        return new User(nickName, email, numberPhone, password);
    }

    public void Update(User user)
    {
        NickName = user.NickName;
        NumberPhone = user.NumberPhone;
        Email = user.Email;
        Password = user.Password;
    }
}
