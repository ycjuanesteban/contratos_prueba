using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Rates.Create;

public class CreateRateCommand : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
}