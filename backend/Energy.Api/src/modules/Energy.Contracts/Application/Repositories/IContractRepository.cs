using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;

namespace Energy.Contracts.Application.Repositories;

public interface IContractRepository
{
    Task<Result<IList<Contract>>> GetAllAsync(CancellationToken cancellationToken);

    Task<Result<Contract>> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<Result> CreateAsync(Contract contract, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Contract contract, CancellationToken cancellationToken);
}
