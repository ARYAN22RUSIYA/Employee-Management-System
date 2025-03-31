using FluentValidation;
using Study_Project.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Employee name is required");

        RuleFor(x => x.Age)
            .InclusiveBetween(18, 65)
            .WithMessage("Age must be between 18 and 65");

    }
}
