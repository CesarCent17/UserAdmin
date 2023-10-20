using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities.DTO
{
    public class DepartmentDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public Guid? CreatedByUserId { get; set; }
    }
}
