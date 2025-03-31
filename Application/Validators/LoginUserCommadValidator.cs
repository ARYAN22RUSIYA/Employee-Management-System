using Application.Features.Authentication.Commands.LoginUser;
using FluentValidation;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .Matches("^[a-zA-Z0-9_@.-]*$")
            .WithMessage("Invalid characters in username.");

    }
}
