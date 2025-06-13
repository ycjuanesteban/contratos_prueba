using Asp.Versioning;
using Energy.Contracts.Application.Users.Create;
using Energy.Contracts.Application.Users.Get;
using Energy.Contracts.Infrastructure.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Energy.Contracts.Infrastructure.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMediator _mediator;

    public UsersController(
        ILogger<UsersController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        _logger.LogInformation("Processing get users request");

        var usersResponse = await _mediator.Send(new GetUsersQuery());

        return usersResponse.IsFailure ?
            BadRequest(usersResponse.Error) :
            Ok(usersResponse.Value.Select(u => u.ToViewModel()).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserViewModel user)
    {
        _logger.LogInformation("Processing create user request");

        var usersResponse = await _mediator.Send(new CreateUserCommand()
        {
            Name = user.Name,
            LastName = user.LastName,
            DNI = user.DNI,
        });

        return usersResponse.IsFailure ? 
            BadRequest(usersResponse.Error) : 
            Ok();
    }
}