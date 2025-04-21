using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Study_Project.Application.DTOs;

namespace Study_Project.Application.Features.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, List<EmployeeDto>>
    {
        private readonly JwtContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeListHandler(JwtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees.ToListAsync(cancellationToken);
            return _mapper.Map<List<EmployeeDto>>(employees);
        }
    }
}
