﻿namespace WebApi.Entities.DTO
{
    public class DepartmentWithIdDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid? CreatedByUserId { get; set; }
    }
}
