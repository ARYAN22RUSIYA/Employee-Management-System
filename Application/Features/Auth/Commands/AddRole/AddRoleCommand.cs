using MediatR;

namespace Application.Features.Auth.Commands.AddRole
{
    public class AddRoleCommand : IRequest<string>
    {
        public string Role { get; set; }
    }
}
