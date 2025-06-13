using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Application.Contracts.Create;

public class CreateContractHandler : IRequestHandler<CreateContractCommand, Result>
{
    private readonly ILogger<CreateContractHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRateRepository _rateRepository;
    private readonly IContractRepository _contractRepository;

    public CreateContractHandler(
        ILogger<CreateContractHandler> logger,
        IUserRepository userRepository,
        IRateRepository rateRepository,
        IContractRepository contractRepository)
    {

        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(userRepository);
        ArgumentNullException.ThrowIfNull(rateRepository);
        ArgumentNullException.ThrowIfNull(contractRepository);

        _logger = logger;
        _userRepository = userRepository;
        _rateRepository = rateRepository;
        _contractRepository = contractRepository;
    }

    public async Task<Result> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateContractHandler] Creating contract");

        var userExistResponse = await _userRepository.GetUserAsync(request.UserId, cancellationToken);

        if (userExistResponse.IsFailure)
        {
            return Result.Failure("User not found");
        }

        var rateExistResponse = await _rateRepository.GetRateAsync(request.RateId, cancellationToken);

        if (rateExistResponse.IsFailure)
        {
            return Result.Failure("Rate not found");
        }

        var contract = Contract.Create(Guid.NewGuid(), userExistResponse.Value, rateExistResponse.Value, request.HiringDate);

        var contractResponse = await _contractRepository.CreateAsync(contract, cancellationToken);
        if(contractResponse.IsFailure)
        {
            return Result.Failure("Error creating the contract");
        }

        return Result.Success(contractResponse);

    }
}