using Study_Project.Application.DTOs;
using MediatR;

namespace Study_Project.Application.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; set; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
