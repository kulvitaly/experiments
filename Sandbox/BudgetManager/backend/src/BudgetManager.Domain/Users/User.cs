using BudgetManager.Domain.Common;
using BudgetManager.Domain.Families;
using BudgetManager.Domain.Wallets;

namespace BudgetManager.Domain.Users;

public class User : BaseEntity<UserId>, IAggregateRoot
{
    private readonly List<WalletId> _walletIds = [];

    public string Name { get; private set; }
    public string IconUrl { get; private set; }
    public FamilyId FamilyId { get; private set; }
    public IReadOnlyCollection<WalletId> WalletIds => _walletIds.AsReadOnly();

    public User(string name, string iconUrl, FamilyId familyId)
    {
        Id = new UserId();
        Name = name;
        IconUrl = iconUrl;
        FamilyId = familyId;
    }

    public void AddWallet(WalletId walletId)
    {
        if (!_walletIds.Contains(walletId))
        {
            _walletIds.Add(walletId);
        }
    }

    public void RemoveWallet(WalletId walletId) => _walletIds.Remove(walletId);
}
