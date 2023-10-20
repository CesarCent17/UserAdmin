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

        public DepartmentService(IDepartmentRepository departmentRepository, IUserRepository IUserRepository)
        {
            _departmentRepository = departmentRepository;
            _userRepository = IUserRepository;
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

            var department = new Department
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                IsActive = departmentDto.IsActive,
                CreatedByUserId = departmentDto.CreatedByUserId
            };

            var response = await _departmentRepository.AddDepartmentAsync(department);

            var departmentCreated = new DepartmentWithIdDto
            {
                Id = response.Data.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                IsActive = departmentDto.IsActive,
                CreatedByUserId = departmentDto.CreatedByUserId
            };

            return response.Succeeded
                ? new ApiResponse<DepartmentWithIdDto>(true, "Se ha creado el registro con éxito", departmentCreated, 200)
                : new ApiResponse<DepartmentWithIdDto>(false, response.ErrorMessage, null, 500);
        }



        public async Task<ApiResponse<Guid>> DeleteDepartmentAsync(Guid id)
        {
            var response = await _departmentRepository.DeleteDepartmentAsync(id);
            if (!response.Succeeded)
            {
                return new ApiResponse<Guid>(false, response.ErrorMessage, id, 500);
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

            var departmentDtos = response.Data.Select(department => new DepartmentWithIdDto
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                IsActive = department.IsActive,
                CreatedByUserId = department.CreatedByUserId
            }).ToList();

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

            var department = response.Data;

            var departmentDto = new DepartmentWithIdDto
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                IsActive = department.IsActive,
                CreatedByUserId = department.CreatedByUserId
            };

            return new ApiResponse<DepartmentWithIdDto>(true, "Obtuvimos el departamento con éxito", departmentDto, 200);
        }
    

        public Task<ApiResponse<Department>> UpdateDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
