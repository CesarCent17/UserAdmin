using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using WebApi.Entities;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class JobTitleService : IJobTitleService
    {
        public Task<ApiResponse<JobTitle>> AddJobTitleAsync(JobTitle jobTitle)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Guid>> DeleteJobTitleAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<JobTitle>>> GetAllJobTitlesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<JobTitle>> GetJobTitleByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<JobTitle>> UpdateJobTitleAsync(JobTitle jobTitle)
        {
            throw new NotImplementedException();
        }
    }
}
