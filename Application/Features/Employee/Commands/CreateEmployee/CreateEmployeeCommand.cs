using Study_Project.Application.DTOs;
using MediatR;

namespace Study_Project.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<EmployeeDto>
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public DateTime JoiningDate { get; set; }
        // just confirming
    }
}
