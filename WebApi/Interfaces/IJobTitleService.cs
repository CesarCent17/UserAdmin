using DataAccess.Entities;
using WebApi.Entities;

namespace WebApi.Interfaces
{
    public interface IJobTitleService
    {
        Task<ApiResponse<JobTitle>> AddJobTitleAsync(JobTitle jobTitle);
        Task<ApiResponse<JobTitle>> GetJobTitleByIdAsync(Guid id);
        Task<ApiResponse<IEnumerable<JobTitle>>> GetAllJobTitlesAsync();
        Task<ApiResponse<JobTitle>> UpdateJobTitleAsync(JobTitle jobTitle);
        Task<ApiResponse<Guid>> DeleteJobTitleAsync(Guid id);
    }
}
