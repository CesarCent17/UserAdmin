using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid JobTitleId { get; set; }

        // Navigation properties - Propiedades de navegacion
        public Department Department { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
