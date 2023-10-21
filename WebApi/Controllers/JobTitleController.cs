using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Entities.DTO;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/jobtitle")]
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleService _jobTitleService;

        public JobTitleController(IJobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<JobTitleWithIdDto>>> GetJobTitleByIdAsync(Guid id)
        {
            var response = await _jobTitleService.GetJobTitleByIdAsync(id);

            if (!response.Succeeded)
            {
                var statusCode = response.StatusCode == 404 ? 404 : 500;
                return StatusCode(statusCode, response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<JobTitleWithIdDto>>>> GetAllJobTitlesAsync()
        {
            var response = await _jobTitleService.GetAllJobTitlesAsync();

            if (!response.Succeeded)
            {
                return StatusCode(500, new ApiResponse<IEnumerable<JobTitleWithIdDto>>(false, response.Message, null, 500));
            }

            return Ok(new ApiResponse<IEnumerable<JobTitleWithIdDto>>(true, "Obtuvimos la lista de títulos de trabajo con éxito", response.Data, 200));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<JobTitleWithIdDto>>> Create(JobTitleDto jobTitleDto)
        {
            var result = await _jobTitleService.AddJobTitleAsync(jobTitleDto);

            if (!result.Succeeded)
            {
                return result.StatusCode == 404
                    ? NotFound(result)
                    : StatusCode(result.StatusCode, result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<JobTitleWithIdDto>>> Update([FromRoute] Guid id, JobTitleUpdateDto jobTitleUpdateDto)
        {
            var result = await _jobTitleService.UpdateJobTitleAsync(id, jobTitleUpdateDto);

            if (!result.Succeeded)
            {
                return result.StatusCode == 404
                    ? NotFound(result)
                    : StatusCode(result.StatusCode, result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Guid>>> Delete(Guid id)
        {
            var result = await _jobTitleService.DeleteJobTitleAsync(id);

            if (!result.Succeeded)
            {
                return result.StatusCode == 404
                    ? NotFound(result)
                    : StatusCode(result.StatusCode, result);
            }

            return Ok(result);
        }
    }
}
