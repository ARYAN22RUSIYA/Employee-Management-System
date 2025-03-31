using MediatR;
using System.Collections.Generic;

namespace Study_Project.Application.Features.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<List<Core.Entities.Employee>>
    {
        // Add filter properties if needed in future
    }
}
