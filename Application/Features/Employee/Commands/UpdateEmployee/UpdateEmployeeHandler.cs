using System.Security.Claims;
using Core.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Study_Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Employee?>
    {
        private readonly JwtContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateEmployeeHandler(JwtContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<Employee?> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var username = user?.FindFirst(ClaimTypes.Name)?.Value
                         ?? user?.FindFirst("sub")?.Value
                         ?? "Unknown";

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (employee == null)
                return null;

            employee.Name = request.Name;
            employee.Dob = request.Dob;
            employee.Age = CalculateAge(request.Dob);
            employee.JoiningDate = request.JoiningDate;
            employee.UpdatedOn = DateTime.UtcNow;
            employee.UpdatedBy = username;


            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age))
                age--;

            return age;
        }

    }
}
