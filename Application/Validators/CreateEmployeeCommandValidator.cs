using FluentValidation;
using Study_Project.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Employee name is required");

        RuleFor(x => x.Dob)
            .Must(dob =>
            {
                var age = DateTime.Now.Year - dob.Year;
                if (dob > DateTime.Now.AddYears(-age)) age--; // account for birthdate not yet reached this year
                return age >= 18 && age <= 65;
            })
            .WithMessage("Employee must be between 18 and 65 years old.");


    }
}
