using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Rates.Get;

public class GetRatesQuery : IRequest<Result<IList<Rate>>>
{ }