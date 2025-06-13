using Asp.Versioning;
using Energy.Contracts.Application.Rates.Create;
using Energy.Contracts.Application.Rates.Get;
using Energy.Contracts.Application.Users.Create;
using Energy.Contracts.Infrastructure.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Infrastructure.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RatesController : ControllerBase
{
    private readonly ILogger<RatesController> _logger;
    private readonly IMediator _mediator;

    public RatesController(
        ILogger<RatesController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRates()
    {
        _logger.LogInformation("Processing get rates request");

        var ratesResponse = await _mediator.Send(new GetRatesQuery());

        return ratesResponse.IsFailure ?
            BadRequest(ratesResponse.Error) :
            Ok(ratesResponse.Value.Select(u => u.ToViewModel()).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateRateViewModel rate)
    {
        _logger.LogInformation("Processing create rate request");

        var createRateResponse = await _mediator.Send(new CreateRateCommand()
        {
            Name = rate.Name
        });

        return createRateResponse.IsFailure ?
            BadRequest(createRateResponse.Error) :
            Ok();
    }
}
