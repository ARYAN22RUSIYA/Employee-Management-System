using MediatR;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Employee.Queries.GetEmployeeList
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
