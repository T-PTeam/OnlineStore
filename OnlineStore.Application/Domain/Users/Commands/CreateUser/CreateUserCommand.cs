using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Users.Common;
using OnlineStore.Core.Domain.Users.Models;

namespace OnlineStore.Application.Domain.Users.Commands.CreateUser;

public class CreateUserCommand : ICreateUserCommand
{
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommand(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public long CreateUser(string nickName, string numberPhone, string email, string password)
    {
        var user = User.Create(nickName, numberPhone, email, password);
        _userRepository.Add(user);
        _unitOfWork.SaveChanges();
        return user.Id;
    }
}