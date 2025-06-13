using FluentValidation;

namespace Energy.Contracts.Application.Contracts.Update;

public class UpdateContractValidator : AbstractValidator<UpdateContractCommand>
{
    public UpdateContractValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Contract id is required");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id is required");
        RuleFor(x => x.RateId).NotEmpty().WithMessage("Rate id is required");
    }
}
