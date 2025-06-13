using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;

namespace Energy.Contracts.Application.Repositories;

public interface IUserRepository
{
    Task<Result<IList<User>>> GetUsersAsync(CancellationToken cancellationToken);

    Task<Result<User>> GetUserAsync(Guid id, CancellationToken cancellationToken);

    Task<Result> CreateUserAsync(User user, CancellationToken cancellationToken);
}