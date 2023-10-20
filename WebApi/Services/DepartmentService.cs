using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using WebApi.Entities;
using WebApi.Entities.DTO;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IUserRepository IUserRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _userRepository = IUserRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<DepartmentWithIdDto>> AddDepartmentAsync(DepartmentDto departmentDto)
        {
            if (departmentDto.CreatedByUserId.HasValue)
            {
                var userFound = await _userRepository.GetUserByIdAsync(departmentDto.CreatedByUserId.Value);

                if (!userFound.Succeeded)
                {
                    var statusCode = userFound.ErrorMessage == "NotFound" ? 404 : 500;
                    return new ApiResponse<DepartmentWithIdDto>(false, userFound.ErrorMessage, null, statusCode);
                }
            }
            var department = _mapper.Map<Department>(departmentDto);
            var response = await _departmentRepository.AddDepartmentAsync(department);
            var departmentCreated = _mapper.Map<DepartmentWithIdDto>(department);
            return response.Succeeded
                ? new ApiResponse<DepartmentWithIdDto>(true, "Se ha creado el registro con éxito", departmentCreated, 200)
                : new ApiResponse<DepartmentWithIdDto>(false, response.ErrorMessage, null, 500);
        }

        public async Task<ApiResponse<Guid>> DeleteDepartmentAsync(Guid id)
        {
            var response = await _departmentRepository.DeleteDepartmentAsync(id);
            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<Guid>(false, response.ErrorMessage, Guid.Empty, statusCode);
            }

            return new ApiResponse<Guid>(true, "Se ha eliminado el departamento con éxito", id, 200);
        }

        public async Task<ApiResponse<IEnumerable<DepartmentWithIdDto>>> GetAllDepartmentsAsync()
        {
            var response = await _departmentRepository.GetAllDepartmentsAsync();

            if (!response.Succeeded)
            {
                return new ApiResponse<IEnumerable<DepartmentWithIdDto>>(false, response.ErrorMessage, null, 500);
            }
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentWithIdDto>>(response.Data);
            return new ApiResponse<IEnumerable<DepartmentWithIdDto>>(true, "Obtuvimos la lista de departamentos con éxito", departmentDtos, 200);
        }

        public async Task<ApiResponse<DepartmentWithIdDto>> GetDepartmentByIdAsync(Guid id)
        {
            var response = await _departmentRepository.GetDepartmentByIdAsync(id);

            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<DepartmentWithIdDto>(false, response.ErrorMessage, null, statusCode);
            }

            var departmentDto = _mapper.Map<DepartmentWithIdDto>(response.Data);
            return new ApiResponse<DepartmentWithIdDto>(true, "Obtuvimos el departamento con éxito", departmentDto, 200);
        }

        public async Task<ApiResponse<DepartmentWithIdDto>> UpdateDepartmentAsync(Guid id, DepartmentUpdateDto departmentUpdateDto)
        {
            var department = _mapper.Map<Department>(departmentUpdateDto);
            var response = await _departmentRepository.UpdateDepartmentAsync(id, department);
            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<DepartmentWithIdDto>(false, response.ErrorMessage, null, statusCode);
            }
            var departmentResponse = _mapper.Map<DepartmentWithIdDto>(response.Data);
            return new ApiResponse<DepartmentWithIdDto>(true, "Se ha actualizado el departamento con éxito", departmentResponse, 200);
        }
    }
}
