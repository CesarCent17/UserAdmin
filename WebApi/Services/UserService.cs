using DataAccess.Entities;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        public Task<ApiResponse<User>> AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Guid>> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<User>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<User>> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<User>> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
