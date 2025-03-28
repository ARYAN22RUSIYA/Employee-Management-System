using MediatR;

namespace Application.Features.Authentication.Commands.AddRole
{
    public class AddRoleCommand : IRequest<string>
    {
        public string Role { get; }

        public AddRoleCommand(string role)
        {
            Role = role;
        }
    }
}
