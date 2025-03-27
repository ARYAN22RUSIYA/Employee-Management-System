using MediatR;

namespace Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
