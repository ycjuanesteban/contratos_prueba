using Energy.Helpers.Entities;

namespace Energy.Contracts.Domain.Entities;

public class Rate : BaseEntity
{
    public string Name { get; protected set; }

    private Rate(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Rate Create(Guid id, string name)
    {
        return new Rate(id, name);
    }
}
