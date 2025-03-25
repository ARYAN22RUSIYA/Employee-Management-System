using Core.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Study_Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Employee?>
    {
        private readonly JwtContext _context;

        public UpdateEmployeeHandler(JwtContext context)
        {
            _context = context;
        }

        public async Task<Employee?> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (employee == null)
                return null;

            employee.Name = request.Name;
            employee.Dob = request.Dob;
            employee.JoiningDate = request.JoiningDate;
            employee.Age = request.Age;
            employee.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }
    }
}
