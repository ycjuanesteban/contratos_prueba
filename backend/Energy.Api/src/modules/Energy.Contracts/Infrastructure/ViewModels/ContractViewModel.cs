using Energy.Contracts.Domain.Entities;

namespace Energy.Contracts.Infrastructure.ViewModels;
public class CreateContractViewModel
{
    public Guid UserId { get; set; }
    public Guid RateId { get; set; }
    public DateTime HiringDate { get; set; }
}

public class UpdateContractViewModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RateId { get; set; }
    public DateTime HiringDate { get; set; }
}

public class ContractViewModel
{
    public Guid Id { get; set; }
    public UserViewModel User { get; set; }
    public RateViewModel Rate { get; set; }
    public DateTime HiringDate { get; set; }
}

public static class ContractViewModelMapper
{
    public static ContractViewModel ToViewModel(this Contract contract)
    {
        return new ContractViewModel
        {
            Id = contract.Id,
            HiringDate = contract.HiringDate,
            Rate = contract.Rate.ToViewModel(),
            User = contract.User.ToViewModel()
        };
    }
}