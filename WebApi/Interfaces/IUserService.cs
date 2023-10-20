using DataAccess.Entities;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<User>> AddUserAsync(User user);
        Task<ApiResponse<User>> GetUserByIdAsync(Guid id);
        Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync();
        Task<ApiResponse<User>> UpdateUserAsync(User user);
        Task<ApiResponse<Guid>> DeleteUserAsync(Guid id);
    }
}
