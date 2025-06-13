using Energy.Contracts.Application.Contracts.Create;
using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Application.Contracts.GetAll;

public class GetContractsHandler : IRequestHandler<GetContractsQuery, Result<IList<Contract>>>
{
    private readonly ILogger<CreateContractHandler> _logger;
    private readonly IContractRepository _contractRepository;

    public GetContractsHandler(
        ILogger<CreateContractHandler> logger,
        IContractRepository contractRepository)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(contractRepository);

        _logger = logger;
        _contractRepository = contractRepository;
    }

    public async Task<Result<IList<Contract>>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GetContractsHandler] Getting contracts");
        
        var contractsResponse = await _contractRepository.GetAllAsync(cancellationToken);

        return contractsResponse.IsFailure ? 
            contractsResponse :
            Result.Success(contractsResponse.Value);
    }
}