using FluentValidation;

namespace Energy.Contracts.Application.Users.Create;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last name is required");
        RuleFor(x => x.DNI).NotNull().NotEmpty().WithMessage("Dni is required");
    }
}