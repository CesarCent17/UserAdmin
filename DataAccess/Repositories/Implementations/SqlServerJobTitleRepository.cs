using DataAccess.DataSource.SqlServer;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations
{
    public class SqlServerJobTitleRepository : IJobTitleRepository
    {
        private readonly TestDbContext _dbContext;

        public SqlServerJobTitleRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<JobTitle>> AddJobTitleAsync(JobTitle jobTitle)
        {
            try
            {
                _dbContext.JobTitles.Add(jobTitle);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<JobTitle> { Succeeded = true, Data = jobTitle };
            }
            catch (Exception ex)
            {
                return new OperationResult<JobTitle> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<Guid>> DeleteJobTitleAsync(Guid id)
        {
            var result = await GetJobTitleByIdAsync(id);
            if (!result.Succeeded) return new OperationResult<Guid> { Succeeded = false, ErrorMessage = result.ErrorMessage };

            try
            {
                _dbContext.JobTitles.Remove(result.Data);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<Guid> { Succeeded = true, Data = id };
            }
            catch (Exception ex)
            {
                return new OperationResult<Guid> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<IEnumerable<JobTitle>>> GetAllJobTitlesAsync()
        {
            try
            {
                var jobTitles = await _dbContext.JobTitles.ToListAsync();
                return new OperationResult<IEnumerable<JobTitle>> { Succeeded = true, Data = jobTitles };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<JobTitle>> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<JobTitle>> GetJobTitleByIdAsync(Guid id)
        {
            try
            {
                var jobTitle = await _dbContext.JobTitles.FirstOrDefaultAsync(jt => jt.Id == id);
                if (jobTitle != null)
                {
                    return new OperationResult<JobTitle> { Succeeded = true, Data = jobTitle };
                }
                else
                {
                    return new OperationResult<JobTitle> { Succeeded = false, ErrorMessage = "Título de trabajo no encontrado" };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<JobTitle> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<JobTitle>> UpdateJobTitleAsync(JobTitle jobTitle)
        {
            try
            {
                _dbContext.JobTitles.Update(jobTitle);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<JobTitle> { Succeeded = true, Data = jobTitle };
            }
            catch (Exception ex)
            {
                return new OperationResult<JobTitle> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }
    }
}
