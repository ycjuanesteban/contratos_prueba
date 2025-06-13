using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Application.Users.Create;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(ILogger<CreateUserHandler> logger, IUserRepository userRepository)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(userRepository);

        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateUserHandler] Creating user");

        var createUserResponse = await _userRepository.CreateUserAsync(User.Create(Guid.NewGuid(), request.Name, request.LastName, request.DNI), cancellationToken);

        if (createUserResponse.IsFailure)
        {
            return Result.Failure("Error creating the user");
        }

        return Result.Success();
    }
}