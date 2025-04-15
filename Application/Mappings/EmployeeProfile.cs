using AutoMapper;
using Core.Entities;
using Study_Project.Application.DTOs;

namespace Study_Project.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
