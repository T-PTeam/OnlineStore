using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Users.Common;
using OnlineStore.Core.Domain.Users.Models;

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

    public async Task UpdateUser(User user)
    {
        var original = await _userRepository.Find(user.Id);

        original.Update(user);
        await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
    }
}
