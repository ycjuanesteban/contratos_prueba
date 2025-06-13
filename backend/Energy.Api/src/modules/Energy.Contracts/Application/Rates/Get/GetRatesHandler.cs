using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Rates.Get;

public class GetRatesHandler : IRequestHandler<GetRatesQuery, Result<IList<Rate>>>
{
    private readonly IRateRepository _rateRepository;

    public GetRatesHandler(IRateRepository rateRepository)
    {
        ArgumentNullException.ThrowIfNull(rateRepository);

        _rateRepository = rateRepository;
    }

    public async Task<Result<IList<Rate>>> Handle(GetRatesQuery request, CancellationToken cancellationToken)
    {
        var ratesResponse = await _rateRepository.GetRatesAsync(cancellationToken);

        if (ratesResponse.IsFailure)
        {
            return Result.Failure<IList<Rate>>("Something went wrong");
        }

        return ratesResponse;
    }
}