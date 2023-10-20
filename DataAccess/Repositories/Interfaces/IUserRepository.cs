using DataAccess.Entities;
using DataAccess.Utils;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<OperationResult<User>> GetUserByIdAsync(Guid id);
        Task<OperationResult<IEnumerable<User>>> GetAllUsersAsync();
        Task<OperationResult<User>> AddUserAsync(User user);
        Task<OperationResult<User>> UpdateUserAsync(Guid id, User user);
        Task<OperationResult<Guid>> DeleteUserAsync(Guid id);
    }
}
