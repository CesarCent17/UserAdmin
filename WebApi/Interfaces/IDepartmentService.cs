using DataAccess.Entities;
using DataAccess.Utils;
using WebApi.Entities;
using WebApi.Entities.DTO;

namespace WebApi.Interfaces
{
    public interface IDepartmentService
    {
        Task<ApiResponse<DepartmentWithIdDto>> AddDepartmentAsync(DepartmentDto departmentDto);
        Task<ApiResponse<DepartmentWithIdDto>> GetDepartmentByIdAsync(Guid id);
        Task<ApiResponse<IEnumerable<DepartmentWithIdDto>>> GetAllDepartmentsAsync();
        Task<ApiResponse<Department>> UpdateDepartmentAsync(Department department);
        Task<ApiResponse<Guid>> DeleteDepartmentAsync(Guid id);
    }
}
