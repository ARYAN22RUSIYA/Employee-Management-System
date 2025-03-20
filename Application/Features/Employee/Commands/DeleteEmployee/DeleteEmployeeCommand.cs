using MediatR;

namespace Study_Project.Application.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteEmployeeCommand(int id)
        {
            Id = id;
        }
    }
}
