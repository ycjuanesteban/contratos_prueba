using Energy.Contracts.Domain.Entities;

namespace Energy.Contracts.Infrastructure.ViewModels;

public class RateViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateRateViewModel
{
    public string Name { get; set; } = string.Empty;
}

public static class RateViewModelMapper
{
    public static RateViewModel ToViewModel(this Rate rate)
    {
        return new RateViewModel
        {
            Id = rate.Id,
            Name = rate.Name
        };
    }
}