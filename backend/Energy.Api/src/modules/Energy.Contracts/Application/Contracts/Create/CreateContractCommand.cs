using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Contracts.Create;

public class CreateContractCommand : IRequest<Result>
{
    public Guid UserId { get; set; }
    public Guid RateId { get; set; }
    public DateTime HiringDate { get; set; }
}