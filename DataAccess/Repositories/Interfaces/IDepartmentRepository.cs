using DataAccess.Entities;
using DataAccess.Utils;

namespace DataAccess.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<OperationResult<Department>> GetDepartmentByIdAsync(Guid id);
        Task<OperationResult<IEnumerable<Department>>> GetAllDepartmentsAsync();
        Task<OperationResult<Department>> AddDepartmentAsync(Department department);
        Task<OperationResult<Department>> UpdateDepartmentAsync(Guid id, Department department);
        Task<OperationResult<Guid>> DeleteDepartmentAsync(Guid id);
    }
}
