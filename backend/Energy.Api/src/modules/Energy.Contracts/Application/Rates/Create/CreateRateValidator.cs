using FluentValidation;

namespace Energy.Contracts.Application.Rates.Create;

public class CreateRateValidator : AbstractValidator<CreateRateCommand>
{
    public CreateRateValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
    }
}
