using Energy.Contracts.Application.Contracts.Create;
using Energy.Contracts.Application.Repositories;
using Energy.Helpers.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Application.Contracts.Update;

public class UpdateContractHandler : IRequestHandler<UpdateContractCommand, Result>
{
    private readonly ILogger<CreateContractHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRateRepository _rateRepository;
    private readonly IContractRepository _contractRepository;

    public UpdateContractHandler(
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

    public async Task<Result> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[UpdateContractHandler] Updating contract");

        var contractExistResponse = await _contractRepository.GetAsync(request.Id, cancellationToken);
        if (contractExistResponse.IsFailure)
        {
            return Result.Failure("Contract not found");
        }

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

        var contract = contractExistResponse.Value;
        contract.UpdateRate(rateExistResponse.Value);
        contract.UpdateUser(userExistResponse.Value);
        contract.UpdateHiringDate(request.HiringDate);

        var contractResponse = await _contractRepository.UpdateAsync(contract, cancellationToken);
        if (contractResponse.IsFailure)
        {
            return Result.Failure("Error updating the contract");
        }

        return Result.Success(contractResponse);
    }
}