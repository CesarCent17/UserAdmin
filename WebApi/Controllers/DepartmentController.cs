using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Entities.DTO;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DepartmentWithIdDto>>> GetDepartmentByIdAsync(Guid id)
        {
            var response = await _departmentService.GetDepartmentByIdAsync(id);

            if (!response.Succeeded)
            {
                var statusCode = response.StatusCode == 404 ? 404 : 500;
                return StatusCode(statusCode, response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<DepartmentWithIdDto>>>> GetAllDepartmentsAsync()
        {
            var response = await _departmentService.GetAllDepartmentsAsync();

            if (!response.Succeeded)
            {
                return StatusCode(500, new ApiResponse<IEnumerable<DepartmentWithIdDto>>(false, response.Message, null, 500));
            }

            return Ok(new ApiResponse<IEnumerable<DepartmentWithIdDto>>(true, "Obtuvimos la lista de departamentos con éxito", response.Data, 200));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<DepartmentWithIdDto>>> Create(DepartmentDto departmentDto)
        {
            var result = await _departmentService.AddDepartmentAsync(departmentDto);

            if (!result.Succeeded)
            {
                return result.StatusCode == 404
                    ? NotFound(result)
                    : StatusCode(result.StatusCode, result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<DepartmentWithIdDto>>> Update([FromRoute] Guid id, DepartmentUpdateDto departmentUpdateDto)
        {
            var result = await _departmentService.UpdateDepartmentAsync(id, departmentUpdateDto);

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
            var result = await _departmentService.DeleteDepartmentAsync(id);

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
