using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Wallets;
using MediatR;

namespace BudgetManager.Application.Wallets;

public record EditWalletCommand(Guid Id, string Name, string IconUrl, WalletType Type) : ICommand<Wallet>;

internal sealed class EditWalletCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<EditWalletCommand, Wallet>
{
    public async Task<Wallet> Handle(EditWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await db.Wallets.FindAsync([new WalletId(request.Id)], cancellationToken)
            ?? throw new InvalidOperationException($"Wallet {request.Id} not found.");

        wallet.Update(request.Name, request.IconUrl, request.Type);
        return wallet;
    }
}
