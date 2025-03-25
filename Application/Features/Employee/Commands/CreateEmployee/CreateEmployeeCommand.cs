using Core.Entities;
using MediatR;

namespace Study_Project.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Age { get; set; }
    }
}
