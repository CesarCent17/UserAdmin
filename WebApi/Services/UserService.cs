using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using WebApi.Entities;
using WebApi.Entities.DTO;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IDepartmentRepository departmentRepository, IJobTitleRepository jobTitleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _jobTitleRepository = jobTitleRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserWithIdDto>> AddUserAsync(UserDTO userDto)
        {
            #region Validate department
            var departmentFound = await _departmentRepository.GetDepartmentByIdAsync(userDto.DepartmentId);

            if (!departmentFound.Succeeded)
            {
                var statusCode = departmentFound.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<UserWithIdDto>(false, departmentFound.ErrorMessage, null, statusCode);
            }
            #endregion

            #region Validate jobTitle
            var jobTitleFound = await _jobTitleRepository.GetJobTitleByIdAsync(userDto.JobTitleId);

            if (!jobTitleFound.Succeeded)
            {
                var statusCode = jobTitleFound.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<UserWithIdDto>(false, jobTitleFound.ErrorMessage, null, statusCode);
            }
            #endregion

            var user = _mapper.Map<User>(userDto);
            var response = await _userRepository.AddUserAsync(user);
            var userCreatedDto = _mapper.Map<UserWithIdDto>(response.Data);

            return response.Succeeded
                ? new ApiResponse<UserWithIdDto>(true, "Se ha creado el usuario con éxito", userCreatedDto, 200)
                : new ApiResponse<UserWithIdDto>(false, response.ErrorMessage, null, 500);
        }

        public async Task<ApiResponse<Guid>> DeleteUserAsync(Guid id)
        {
            var response = await _userRepository.DeleteUserAsync(id);
            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<Guid>(false, response.ErrorMessage, Guid.Empty, statusCode);
            }

            return new ApiResponse<Guid>(true, "Se ha eliminado el usuario con éxito", id, 200);
        }

        public async Task<ApiResponse<IEnumerable<UserWithIdDto>>> GetAllUsersAsync()
        {
            var response = await _userRepository.GetAllUsersAsync();

            if (!response.Succeeded)
            {
                return new ApiResponse<IEnumerable<UserWithIdDto>>(false, response.ErrorMessage, null, 500);
            }

            var userWithDtos = _mapper.Map<IEnumerable<UserWithIdDto>>(response.Data);
            return new ApiResponse<IEnumerable<UserWithIdDto>>(true, "Obtuvimos la lista de usuarios con éxito", userWithDtos, 200);
        }

        public async Task<ApiResponse<UserWithIdDto>> GetUserByIdAsync(Guid id)
        {
            var response = await _userRepository.GetUserByIdAsync(id);

            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<UserWithIdDto>(false, response.ErrorMessage, null, statusCode);
            }

            var userWithDto = _mapper.Map<UserWithIdDto>(response.Data);
            return new ApiResponse<UserWithIdDto>(true, "Obtuvimos el usuario con éxito", userWithDto, 200);
        }

        public async Task<ApiResponse<UserWithIdDto>> UpdateUserAsync(Guid id, UserUpdateDTO userUpdateDto)
        {
            #region Validate department
            var departmentFound = await _departmentRepository.GetDepartmentByIdAsync(userUpdateDto.DepartmentId);

            if (!departmentFound.Succeeded)
            {
                var statusCode = departmentFound.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<UserWithIdDto>(false, departmentFound.ErrorMessage, null, statusCode);
            }
            #endregion

            #region Validate jobTitle
            var jobTitleFound = await _jobTitleRepository.GetJobTitleByIdAsync(userUpdateDto.JobTitleId);

            if (!jobTitleFound.Succeeded)
            {
                var statusCode = jobTitleFound.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<UserWithIdDto>(false, jobTitleFound.ErrorMessage, null, statusCode);
            }
            #endregion

            var user = _mapper.Map<User>(userUpdateDto);
            var response = await _userRepository.UpdateUserAsync(id, user);
            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<UserWithIdDto>(false, response.ErrorMessage, null, statusCode);
            }

            var userResponseDto = _mapper.Map<UserWithIdDto>(response.Data);
            return new ApiResponse<UserWithIdDto>(true, "Se ha actualizado el usuario con éxito", userResponseDto, 200);
        }
    }
}
