using BudgetManager.Domain.Common;
using BudgetManager.Domain.Families;

namespace BudgetManager.Domain.Users;

public class User : BaseEntity<UserId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string IconUrl { get; private set; }
    public FamilyId? FamilyId { get; private set; }

    private User() { Name = string.Empty; IconUrl = string.Empty; }

    public User(string name, string iconUrl)
    {
        Id = new UserId();
        Name = name;
        IconUrl = iconUrl;
    }

    public User(string name, string iconUrl, FamilyId familyId) : this(name, iconUrl)
    {
        FamilyId = familyId;
    }

    public void Update(string name, string iconUrl)
    {
        Name = name;
        IconUrl = iconUrl;
    }
}
