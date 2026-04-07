using BudgetManager.Domain.Common;
using BudgetManager.Domain.Users;

namespace BudgetManager.Domain.Families;

public class Family : BaseEntity<FamilyId>, IAggregateRoot
{
    private readonly List<UserId> _userIds = [];

    public string Name { get; private set; }
    public string IconUrl { get; private set; }
    public IReadOnlyCollection<UserId> UserIds => _userIds.AsReadOnly();

    public Family(string name, string iconUrl)
    {
        Id = new FamilyId();
        Name = name;
        IconUrl = iconUrl;
    }

    public void AddUser(UserId userId)
    {
        if (!_userIds.Contains(userId))
        {
            _userIds.Add(userId);
        }
    }

    public void RemoveUser(UserId userId) => _userIds.Remove(userId);
}
