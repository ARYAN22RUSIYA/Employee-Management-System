using MediatR;
using Study_Project.Core.Entities;

namespace Study_Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<Employee?>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Age { get; set; }
    }
}
