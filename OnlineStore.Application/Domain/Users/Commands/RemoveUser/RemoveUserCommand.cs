using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Users.Common;

namespace OnlineStore.Application.Domain.Users.Commands.RemoveUser;

public class RemoveUserCommand : IRemoveUserCommand
{
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWork _unitOfWork;


    public RemoveUserCommand(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task RemoveUser(long id)
    {
        _userRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }
}