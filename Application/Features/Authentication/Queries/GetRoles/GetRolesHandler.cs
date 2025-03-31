using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Authentication.Queries.GetRoles
{
    public class GetRolesHandler : IRequestHandler<GetRolesQuery, List<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRolesHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            if (!roles.Any())
                throw new System.Exception("No roles found");

            return roles;
        }
    }
}
