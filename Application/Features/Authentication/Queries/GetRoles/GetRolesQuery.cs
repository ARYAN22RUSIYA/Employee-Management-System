using MediatR;

namespace Application.Features.Authentication.Queries.GetRoles
{
    public class GetRolesQuery : IRequest<List<string>>
    {
    }
}
