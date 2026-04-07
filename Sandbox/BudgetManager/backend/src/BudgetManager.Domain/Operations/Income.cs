using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Wallets;

namespace BudgetManager.Domain.Operations;

public class Income : Operation
{
    public WalletId WalletId { get; private set; }
    public CategoryId CategoryId { get; private set; }

    public Income(decimal amount, DateTimeOffset occurredOn, WalletId walletId, CategoryId categoryId)
        : base(amount, occurredOn)
    {
        WalletId = walletId;
        CategoryId = categoryId;
    }
}
