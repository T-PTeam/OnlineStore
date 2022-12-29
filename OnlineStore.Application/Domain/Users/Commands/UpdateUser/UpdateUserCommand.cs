using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Users.Common;

namespace OnlineStore.Application.Domain.Users.Commands.UpdateUser;

public class UpdateUserCommand : IUpdateUserCommand
{
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommand(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task UpdateUser(long id, string nickName, string numberPhone, string email, string password)
    {
        var user = _userRepository.Find(id);
        user.NickName = nickName;
        user.NumberPhone = numberPhone;
        user.Email = email;
        user.Password = password;
        await _unitOfWork.SaveChangesAsync();
    }

    //TODO UserUpdate
    //public async Task UpdateUser(User user)
    //{
    //    var original = _userRepository.Find(user.Id);

    //    original.Update(user);
    //    _userRepository.Update(original);
    //    await _unitOfWork.SaveChangesAsync();
    //}
}
