using FluentValidation;

namespace Energy.Contracts.Application.Contracts.Create;

public class CreateContractValidator : AbstractValidator<CreateContractCommand>
{
    public CreateContractValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEmpty().WithMessage("User id is required");
        RuleFor(x => x.RateId).NotEmpty().NotEmpty().WithMessage("Rate id is required");
        RuleFor(x => x.HiringDate).NotEmpty().NotEmpty().WithMessage("Hiring date is required");
    }
}