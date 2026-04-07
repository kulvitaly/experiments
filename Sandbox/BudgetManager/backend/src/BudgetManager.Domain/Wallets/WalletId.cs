using BudgetManager.Domain.Common;

namespace BudgetManager.Domain.Wallets;

public record WalletId(Guid Value) : Key<Guid>(Value)
{
    public WalletId() : this(Guid.CreateVersion7()) { }
}
