namespace WebApi.Entities.DTO
{
    public class UserWithIdDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public DepartmentWithIdDto Department { get; set; }
        public JobTitleWithIdDto JobTitle { get; set; }
    }
}
