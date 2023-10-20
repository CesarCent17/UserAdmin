using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Entities.DTO;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UserWithIdDto>>> GetUserByIdAsync(Guid id)
        {
            var response = await _userService.GetUserByIdAsync(id);

            if (!response.Succeeded)
            {
                var statusCode = response.StatusCode == 404 ? 404 : 500;
                return StatusCode(statusCode, response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserWithIdDto>>>> GetAllUsersAsync()
        {
            var response = await _userService.GetAllUsersAsync();

            if (!response.Succeeded)
            {
                return StatusCode(500, new ApiResponse<IEnumerable<UserWithIdDto>>(false, response.Message, null, 500));
            }

            return Ok(new ApiResponse<IEnumerable<UserWithIdDto>>(true, "Obtuvimos la lista de usuarios con éxito", response.Data, 200));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserWithIdDto>>> CreateUser(UserDTO userDto)
        {
            var result = await _userService.AddUserAsync(userDto);

            if (!result.Succeeded)
            {
                return result.StatusCode == 404
                    ? NotFound(result)
                    : StatusCode(result.StatusCode, result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<UserWithIdDto>>> UpdateUser([FromRoute] Guid id, UserUpdateDTO userUpdateDto)
        {
            var result = await _userService.UpdateUserAsync(id, userUpdateDto);

            if (!result.Succeeded)
            {
                return result.StatusCode == 404
                    ? NotFound(result)
                    : StatusCode(result.StatusCode, result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Guid>>> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (!result.Succeeded)
            {
                return result.StatusCode == 404
                    ? NotFound(result)
                    : StatusCode(result.StatusCode, result);
            }
            return Ok(result);
        }
    }
}
