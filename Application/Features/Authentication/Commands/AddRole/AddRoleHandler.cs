using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.AddRole
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, string>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<string> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            if (await _roleManager.RoleExistsAsync(request.Role))
                throw new Exception("Role already exists");

            var result = await _roleManager.CreateAsync(new IdentityRole(request.Role));
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return "Role added successfully";
        }
    }
}
