using DataAccess.Entities;
using DataAccess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IJobTitleRepository
    {
        Task<OperationResult<JobTitle>> GetJobTitleByIdAsync(Guid id);
        Task<OperationResult<IEnumerable<JobTitle>>> GetAllJobTitlesAsync();
        Task<OperationResult<JobTitle>> AddJobTitleAsync(JobTitle jobTitle);
        Task<OperationResult<JobTitle>> UpdateJobTitleAsync(Guid id, JobTitle jobTitle);
        Task<OperationResult<Guid>> DeleteJobTitleAsync(Guid id);
    }
}
