﻿using AutoMapper;
using DataAccess.Entities;
using WebApi.Entities.DTO;

namespace WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Department, DepartmentWithIdDto>();
            CreateMap<DepartmentUpdateDto, Department>();

            CreateMap<JobTitle, JobTitleDto>().ReverseMap();
            CreateMap<JobTitle, JobTitleWithIdDto>();
            CreateMap<JobTitleUpdateDto, JobTitle>();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserWithIdDto>();
            CreateMap<UserUpdateDTO, User>();
        }
    }
}
