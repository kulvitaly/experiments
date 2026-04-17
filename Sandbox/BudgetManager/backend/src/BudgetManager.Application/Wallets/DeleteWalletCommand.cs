using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Wallets;
using MediatR;

namespace BudgetManager.Application.Wallets;

public record DeleteWalletCommand(Guid Id) : ICommand<bool>;

internal sealed class DeleteWalletCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<DeleteWalletCommand, bool>
{
    public async Task<bool> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await db.Wallets.FindAsync([new WalletId(request.Id)], cancellationToken);
        if (wallet is null)
        {
            return false;
        }

        wallet.SoftDelete();
        return true;
    }
}
