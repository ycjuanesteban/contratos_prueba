using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Contracts.Update
{
    public class UpdateContractCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RateId { get; set; }
        public DateTime HiringDate { get; set; }
    }
}
