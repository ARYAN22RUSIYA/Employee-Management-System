using System.Security.Claims;
using AutoMapper;
using Core.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Study_Project.Application.DTOs;

namespace Study_Project.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly JwtContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateEmployeeHandler(JwtContext context, IHttpContextAccessor httpContextAccessor , IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
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
            return _mapper.Map<EmployeeDto>(employee);
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
