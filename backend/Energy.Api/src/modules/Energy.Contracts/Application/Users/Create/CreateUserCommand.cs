using Energy.Helpers.Result;
using MediatR;

namespace Energy.Contracts.Application.Users.Create;
public class CreateUserCommand : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
}
