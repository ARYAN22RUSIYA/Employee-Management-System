using MediatR;

namespace Application.Features.Authentication.Commands.LoginUser;

public class LoginUserCommand : IRequest<string>
{
    public string Username { get; }
    public string Password { get; }

    public LoginUserCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
