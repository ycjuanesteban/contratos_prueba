using Asp.Versioning;
using Energy.Contracts.Application.Contracts.Create;
using Energy.Contracts.Application.Contracts.GetAll;
using Energy.Contracts.Application.Contracts.GetOne;
using Energy.Contracts.Application.Contracts.Update;
using Energy.Contracts.Infrastructure.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Infrastructure.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly ILogger<ContractsController> _logger;
    private readonly IMediator _mediator;

    public ContractsController(
        ILogger<ContractsController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract(CreateContractViewModel viewModel)
    {
        _logger.LogInformation("Processing create contract request");

        var createContractResponse = await _mediator.Send(new CreateContractCommand()
        {
            RateId = viewModel.RateId,
            UserId = viewModel.UserId,
            HiringDate = viewModel.HiringDate
        });

        return createContractResponse.IsFailure ?
            BadRequest(createContractResponse.Error) :
            Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateContract(UpdateContractViewModel viewModel)
    {
        _logger.LogInformation("Processing update contract request");

        var updateContractResponse = await _mediator.Send(new UpdateContractCommand()
        {
            Id = viewModel.Id,
            RateId = viewModel.RateId,
            UserId = viewModel.UserId,
            HiringDate = viewModel.HiringDate
        });

        return updateContractResponse.IsFailure ?
            BadRequest(updateContractResponse.Error) :
            Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetContracts()
    {
        _logger.LogInformation("Processing get contracts request");

        var getContractsResponse = await _mediator.Send(new GetContractsQuery());

        if (getContractsResponse.IsFailure)
        {
            return BadRequest(getContractsResponse.Error);
        }

        var contracts = getContractsResponse.Value.Select(x => x.ToViewModel()).ToList();
        return Ok(contracts);
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetContracts([FromRoute]Guid id)
    {
        _logger.LogInformation("Processing get contract request");

        var getContractResponse = await _mediator.Send(new GetContractQuery() { Id = id });

        if (getContractResponse.IsFailure)
        {
            return BadRequest(getContractResponse.Error);
        }

        var contract = getContractResponse.Value.ToViewModel();
        return Ok(contract);
    }
}