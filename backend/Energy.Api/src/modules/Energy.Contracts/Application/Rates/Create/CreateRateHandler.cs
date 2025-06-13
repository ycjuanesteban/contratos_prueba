using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Application.Rates.Create;

public class CreateRateHandler : IRequestHandler<CreateRateCommand, Result>
{
    private readonly ILogger<CreateRateHandler> _logger;
    private readonly IRateRepository _rateRepository;

    public CreateRateHandler(
        ILogger<CreateRateHandler> logger,
        IRateRepository rateRepository)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(rateRepository);

        _logger = logger;
        _rateRepository = rateRepository;
    }

    public async Task<Result> Handle(CreateRateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CreateRateHandler] creating rate");

        var createResponse = await _rateRepository.CreateRateAsync(Rate.Create(Guid.NewGuid(), request.Name), cancellationToken);

        return createResponse;

    }
}