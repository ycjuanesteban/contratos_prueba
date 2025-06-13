using Energy.Helpers.Entities;

namespace Energy.Contracts.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; protected set; }
    public string LastName { get; protected set; }
    public string DNI { get; protected set; }

    private User(Guid id, string name, string lastName, string dni)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        DNI = dni;
    }

    public static User Create(Guid id, string name, string lastName, string dni)
    {
        return new User(id, name, lastName,dni);
    }
}