using MediatR;
using Study_Project.Application.DTOs;

namespace Study_Project.Application.Features.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<List<EmployeeDto>>
    {
        // Add filter properties if needed in future
    }
}
