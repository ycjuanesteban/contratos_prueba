using Energy.Contracts.Application.Contracts.Create;
using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Application.Contracts.GetOne;

public class GetContractHandler : IRequestHandler<GetContractQuery, Result<Contract>>
{
    private readonly ILogger<CreateContractHandler> _logger;
    private readonly IContractRepository _contractRepository;

    public GetContractHandler(
        ILogger<CreateContractHandler> logger,
        IContractRepository contractRepository)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(contractRepository);

        _logger = logger;
        _contractRepository = contractRepository;
    }

    public async Task<Result<Contract>> Handle(GetContractQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GetContractHandler] Getting contract");
        
        var contractsResponse = await _contractRepository.GetAsync(request.Id, cancellationToken);

        return contractsResponse.IsFailure ? 
            contractsResponse :
            Result.Success(contractsResponse.Value);
    }
}