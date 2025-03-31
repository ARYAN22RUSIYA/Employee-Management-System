using MediatR;

namespace Application.Features.Authentication.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Role { get; set; }

        public AssignRoleCommand(string username, string role)
        {
            Username = username;
            Role = role;
        }
    }
}
