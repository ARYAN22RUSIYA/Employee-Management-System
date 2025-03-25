using MediatR;
using System.Collections.Generic;

namespace Application.Features.Employee.Queries.GetEmployeeList
{
    public class GetEmployeeListQuery : IRequest<List<Core.Entities.Employee>>
    {
        // Add filter properties if needed in future
    }
}
