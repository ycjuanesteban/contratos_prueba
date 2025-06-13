using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Application.Users.Get;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<IList<User>>>
{

    private readonly ILogger<GetUsersHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(
        ILogger<GetUsersHandler> logger,
        IUserRepository userRepository)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(userRepository);

        _logger = logger;
        _userRepository = userRepository;
    }

    async Task<Result<IList<User>>> IRequestHandler<GetUsersQuery, Result<IList<User>>>.Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {

        _logger.LogInformation("[GetUsersHandler] Getting users");

        var usersResponse = await _userRepository.GetUsersAsync(cancellationToken);

        if (usersResponse.IsFailure)
        {
            return Result.Failure<IList<User>>("Something went wrong");
        }

        return usersResponse;
    }
}