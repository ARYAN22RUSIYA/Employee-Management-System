﻿using MediatR;
using Study_Project.Core.Entities;
using Study_Project.Infrastructure.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Study_Project.Application.Features.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Employee>
    {
        private readonly JwtContext _context;

        public CreateEmployeeHandler(JwtContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Dob = request.Dob,
                JoiningDate = request.JoiningDate,
                Age = request.Age,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return employee;
        }
    }
}
