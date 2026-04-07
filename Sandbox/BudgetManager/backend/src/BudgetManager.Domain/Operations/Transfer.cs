using BudgetManager.Domain.Wallets;

namespace BudgetManager.Domain.Operations;

public class Transfer : Operation
{
    public WalletId SourceWalletId { get; private set; }
    public WalletId DestinationWalletId { get; private set; }

    public Transfer(decimal amount, DateTimeOffset occurredOn, WalletId sourceWalletId, WalletId destinationWalletId)
        : base(amount, occurredOn)
    {
        SourceWalletId = sourceWalletId;
        DestinationWalletId = destinationWalletId;
    }
}
