using DataAccess.DataSource.SqlServer;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class SqlServerDepartmentRepository : IDepartmentRepository
    {
        private readonly TestDbContext _dbContext;

        public SqlServerDepartmentRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<Department>> AddDepartmentAsync(Department department)
        {
            try
            {
                _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<Department> { Succeeded = true, Data = department };
            }
            catch (Exception ex)
            {
                return new OperationResult<Department> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<Guid>> DeleteDepartmentAsync(Guid id)
        {
            var result = await GetDepartmentByIdAsync(id);
            if (!result.Succeeded) return new OperationResult<Guid> { Succeeded = false, ErrorMessage = result.ErrorMessage };

            try
            {
                _dbContext.Departments.Remove(result.Data);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<Guid> { Succeeded = true, Data = id };
            }
            catch (Exception ex)
            {
                return new OperationResult<Guid> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<IEnumerable<Department>>> GetAllDepartmentsAsync()
        {
            try
            {
                var departments = await _dbContext.Departments.ToListAsync();
                return new OperationResult<IEnumerable<Department>> { Succeeded = true, Data = departments };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<Department>> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<Department>> GetDepartmentByIdAsync(Guid id)
        {
            try
            {
                var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
                if (department != null)
                {
                    return new OperationResult<Department> { Succeeded = true, Data = department };
                }
                else
                {
                    return new OperationResult<Department> { Succeeded = false, ErrorMessage = "NotFound" };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<Department> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<Department>> UpdateDepartmentAsync(Department department)
        {
            try
            {
                _dbContext.Departments.Update(department);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<Department> { Succeeded = true, Data = department };
            }
            catch (Exception ex)
            {
                return new OperationResult<Department> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }
    }
}
