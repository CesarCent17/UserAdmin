﻿namespace WebApi.Entities.DTO
{
    public class UserUpdateDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid JobTitleId { get; set; }
    }
}
