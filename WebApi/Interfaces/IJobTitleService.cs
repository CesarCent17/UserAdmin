using WebApi.Entities;
using WebApi.Entities.DTO;

namespace WebApi.Interfaces
{
    public interface IJobTitleService
    {
        Task<ApiResponse<JobTitleWithIdDto>> AddJobTitleAsync(JobTitleDto jobTitleDto);
        Task<ApiResponse<JobTitleWithIdDto>> GetJobTitleByIdAsync(Guid id);
        Task<ApiResponse<IEnumerable<JobTitleWithIdDto>>> GetAllJobTitlesAsync();
        Task<ApiResponse<JobTitleWithIdDto>> UpdateJobTitleAsync(Guid id, JobTitleUpdateDto jobTitleUpdateDto);
        Task<ApiResponse<Guid>> DeleteJobTitleAsync(Guid id);
    }
}
