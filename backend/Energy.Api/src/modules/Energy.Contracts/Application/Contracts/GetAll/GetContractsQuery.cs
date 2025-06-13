using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Contracts.GetAll;

public class GetContractsQuery : IRequest<Result<IList<Contract>>>
{ }