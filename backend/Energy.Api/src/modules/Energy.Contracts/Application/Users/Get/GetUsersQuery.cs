using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Users.Get;

public class GetUsersQuery : IRequest<Result<IList<User>>>
{ }