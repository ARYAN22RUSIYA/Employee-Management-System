using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Study_Project.Application.DTOs;

namespace Study_Project.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly JwtContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdHandler(JwtContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            return _mapper.Map<EmployeeDto>(employee);
        }
    }
}
