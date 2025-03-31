using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Study_Project.Application.Features.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<Core.Entities.Employee>>
    {
        private readonly JwtContext _context;

        public GetEmployeeListHandler(JwtContext context)
        {
            _context = context;
        }

        public async Task<List<Core.Entities.Employee>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }
    }
}
