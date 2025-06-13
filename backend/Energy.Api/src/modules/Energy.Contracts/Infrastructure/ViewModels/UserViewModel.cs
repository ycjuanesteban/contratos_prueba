using Energy.Contracts.Domain.Entities;

namespace Energy.Contracts.Infrastructure.ViewModels;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
}

public class CreateUserViewModel
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
}

public static class UserViewModelMapper
{
    public static UserViewModel ToViewModel(this User user)
    {
        return new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            DNI = user.DNI,
        };
    }
}