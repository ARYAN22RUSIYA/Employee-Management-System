using MediatR;
using Core.Entities;

namespace Application.Features.Employee.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<List<Employee>>
    {
    }
}
