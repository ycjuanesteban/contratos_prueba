using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;

namespace Energy.Contracts.Application.Repositories;

public interface IRateRepository
{
    Task<Result<IList<Rate>>> GetRatesAsync(CancellationToken cancellationToken);

    Task<Result<Rate>> GetRateAsync(Guid id, CancellationToken cancellationToken);

    Task<Result> CreateRateAsync(Rate rate, CancellationToken cancellationToken);
}