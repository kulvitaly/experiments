using BudgetManager.Domain.Common;
using BudgetManager.Domain.Users;

namespace BudgetManager.Domain.Wallets;

public class Wallet : BaseEntity<WalletId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string IconUrl { get; private set; }
    public WalletType Type { get; private set; }
    public UserId OwnerId { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    private Wallet()
    {
        Name = string.Empty;
        IconUrl = string.Empty;
        OwnerId = default!;
    }

    public Wallet(string name, string iconUrl, WalletType type, UserId ownerId)
    {
        Id = new WalletId();
        Name = name;
        IconUrl = iconUrl;
        Type = type;
        OwnerId = ownerId;
    }

    public void Update(string name, string iconUrl, WalletType type)
    {
        Name = name;
        IconUrl = iconUrl;
        Type = type;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}
