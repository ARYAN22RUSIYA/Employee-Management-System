using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, string>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AssignRoleCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                throw new Exception("User not found");

            var result = await _userManager.AddToRoleAsync(user, request.Role);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return "Role assigned successfully";
        }
    }
}
