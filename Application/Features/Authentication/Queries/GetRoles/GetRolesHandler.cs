using MediatR;

namespace Application.Auth.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<string>>
    {
    }
}
