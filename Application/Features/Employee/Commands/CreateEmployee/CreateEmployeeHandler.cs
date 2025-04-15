using System.Security.Claims;
using Core.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Study_Project.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly JwtContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateEmployeeHandler(JwtContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var username = user?.FindFirst(ClaimTypes.Name)?.Value
                         ?? user?.FindFirst("sub")?.Value
                         ?? "Unknown";

            var employee = new Employee
            {
                Name = request.Name,
                Dob = request.Dob,
                Age = CalculateAge(request.Dob),
                JoiningDate = request.JoiningDate,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CreatedBy = username,
                UpdatedBy = username,
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
