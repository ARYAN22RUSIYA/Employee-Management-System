using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Study_Project.Application.DTOs;

namespace Study_Project.Application.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto?>
    {
        private readonly JwtContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UpdateEmployeeHandler(JwtContext context, IHttpContextAccessor httpContextAccessor , IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        public async Task<EmployeeDto?> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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
            return _mapper.Map<EmployeeDto>(employee);
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
