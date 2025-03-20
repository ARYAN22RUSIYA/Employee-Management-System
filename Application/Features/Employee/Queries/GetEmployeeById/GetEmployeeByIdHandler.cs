using MediatR;
using Microsoft.EntityFrameworkCore;
using Study_Project.Core.Entities;
using Study_Project.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Study_Project.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly JwtContext _context;

        public GetEmployeeByIdHandler(JwtContext context)
        {
            _context = context;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            return employee;
        }
    }
}
