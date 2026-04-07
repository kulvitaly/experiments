using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Wallets;

namespace BudgetManager.Domain.Operations;

public class Expense : Operation
{
    public WalletId WalletId { get; private set; }
    public CategoryId CategoryId { get; private set; }

    public Expense(decimal amount, DateTimeOffset occurredOn, WalletId walletId, CategoryId categoryId)
        : base(amount, occurredOn)
    {
        WalletId = walletId;
        CategoryId = categoryId;
    }
}
