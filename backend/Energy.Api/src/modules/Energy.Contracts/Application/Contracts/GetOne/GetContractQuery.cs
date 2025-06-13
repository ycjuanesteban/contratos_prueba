using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Contracts.GetOne;

public class GetContractQuery : IRequest<Result<Contract>>
{
    public Guid Id { get; set; }
}