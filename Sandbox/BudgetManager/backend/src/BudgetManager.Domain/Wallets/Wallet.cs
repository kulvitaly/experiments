using BudgetManager.Domain.Common;
using BudgetManager.Domain.Users;

namespace BudgetManager.Domain.Wallets;

public class Wallet : BaseEntity<WalletId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string IconUrl { get; private set; }
    public decimal Value { get; private set; }
    public UserId OwnerId { get; private set; }

    public Wallet(string name, string iconUrl, UserId ownerId)
    {
        Id = new WalletId();
        Name = name;
        IconUrl = iconUrl;
        OwnerId = ownerId;
        Value = 0;
    }

    public void Deposit(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Value += amount;
    }

    public void Withdraw(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        if (amount > Value)
            throw new InvalidOperationException("Insufficient funds.");
        Value -= amount;
    }
}
