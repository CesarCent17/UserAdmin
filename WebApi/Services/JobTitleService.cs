using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using WebApi.Entities;
using WebApi.Entities.DTO;
using WebApi.Interfaces;

namespace WebApi.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public JobTitleService(IJobTitleRepository jobTitleRepository, IUserRepository userRepository, IMapper mapper)
        {
            _jobTitleRepository = jobTitleRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<JobTitleWithIdDto>> AddJobTitleAsync(JobTitleDto jobTitleDto)
        {
            if (jobTitleDto.CreatedByUserId.HasValue)
            {
                var jobTitleFound = await _userRepository.GetUserByIdAsync(jobTitleDto.CreatedByUserId.Value);

                if (!jobTitleFound.Succeeded)
                {
                    var statusCode = jobTitleFound.ErrorMessage == "NotFound" ? 404 : 500;
                    return new ApiResponse<JobTitleWithIdDto>(false, jobTitleFound.ErrorMessage, null, statusCode);
                }
            }

            var jobTitle = _mapper.Map<JobTitle>(jobTitleDto); 
            var response = await _jobTitleRepository.AddJobTitleAsync(jobTitle);
            var jobTitleCreatedDto = _mapper.Map<JobTitleWithIdDto>(response.Data);

            return response.Succeeded
                ? new ApiResponse<JobTitleWithIdDto>(true, "Se ha creado el título de trabajo con éxito", jobTitleCreatedDto, 200)
                : new ApiResponse<JobTitleWithIdDto>(false, response.ErrorMessage, null, 500);
        }

        public async Task<ApiResponse<Guid>> DeleteJobTitleAsync(Guid id)
        {
            var response = await _jobTitleRepository.DeleteJobTitleAsync(id);
            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<Guid>(false, response.ErrorMessage, Guid.Empty, statusCode);
            }

            return new ApiResponse<Guid>(true, "Se ha eliminado el título de trabajo con éxito", id, 200);
        }

        public async Task<ApiResponse<IEnumerable<JobTitleWithIdDto>>> GetAllJobTitlesAsync()
        {
            var response = await _jobTitleRepository.GetAllJobTitlesAsync();

            if (!response.Succeeded)
            {
                return new ApiResponse<IEnumerable<JobTitleWithIdDto>>(false, response.ErrorMessage, null, 500);
            }

            var jobTitleWithDtos = _mapper.Map<IEnumerable<JobTitleWithIdDto>>(response.Data);
            return new ApiResponse<IEnumerable<JobTitleWithIdDto>>(true, "Obtuvimos la lista de títulos de trabajo con éxito", jobTitleWithDtos, 200);
        }

        public async Task<ApiResponse<JobTitleWithIdDto>> GetJobTitleByIdAsync(Guid id)
        {
            var response = await _jobTitleRepository.GetJobTitleByIdAsync(id);

            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<JobTitleWithIdDto>(false, response.ErrorMessage, null, statusCode);
            }

            var jobTitleWithDto = _mapper.Map<JobTitleWithIdDto>(response.Data);
            return new ApiResponse<JobTitleWithIdDto>(true, "Obtuvimos el título de trabajo con éxito", jobTitleWithDto, 200);
        }

        public async Task<ApiResponse<JobTitleWithIdDto>> UpdateJobTitleAsync(Guid id, JobTitleUpdateDto jobTitleUpdateDto)
        {
            var jobTitle = _mapper.Map<JobTitle>(jobTitleUpdateDto);
            var response = await _jobTitleRepository.UpdateJobTitleAsync(id, jobTitle);
            if (!response.Succeeded)
            {
                var statusCode = response.ErrorMessage == "NotFound" ? 404 : 500;
                return new ApiResponse<JobTitleWithIdDto>(false, response.ErrorMessage, null, statusCode);
            }

            var jobTitleResponseDto = _mapper.Map<JobTitleWithIdDto>(response.Data);

            return new ApiResponse<JobTitleWithIdDto>(true, "Se ha actualizado el título de trabajo con éxito", jobTitleResponseDto, 200);
        }
    }
}
