using MediatR;
using System.Collections.Generic;

namespace Application.Auth.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<string>>
    {
    }
}
