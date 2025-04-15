using Study_Project.Application.DTOs;
using MediatR;

namespace Study_Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<EmployeeDto?>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
