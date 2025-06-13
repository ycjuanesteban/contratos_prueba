using Energy.Helpers.Entities;

namespace Energy.Contracts.Domain.Entities;

public class Contract : BaseEntity
{
    public User User { get; protected set; }
    public Rate Rate { get; protected set; }
    public DateTime HiringDate { get; protected set; }

    private Contract(Guid id, User user, Rate rate, DateTime hiringDate)
    {
        Id = id;
        User = user;
        Rate = rate;
        HiringDate = hiringDate;
    }

    public static Contract Create(Guid id, User user, Rate rate, DateTime hiringDate)
    {
        return new Contract(id, user, rate, hiringDate);
    }

    public void UpdateUser(User user)
    {
        User = user;
    }

    public void UpdateRate(Rate rate)
    {
        Rate = rate;
    }

    public void UpdateHiringDate(DateTime hiringDate)
    {
        HiringDate = hiringDate;
    }
}
