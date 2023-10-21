using WebApi.Entities;
using WebApi.Entities.DTO;

namespace WebApi.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserWithIdDto>> AddUserAsync(UserDTO userDto);
        Task<ApiResponse<UserWithIdDto>> GetUserByIdAsync(Guid id);
        Task<ApiResponse<IEnumerable<UserWithIdDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserWithIdDto>> UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDto);
        Task<ApiResponse<Guid>> DeleteUserAsync(Guid id);
    }
}