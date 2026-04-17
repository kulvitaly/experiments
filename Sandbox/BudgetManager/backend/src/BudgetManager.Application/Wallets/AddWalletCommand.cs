using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Users;
using BudgetManager.Domain.Wallets;
using MediatR;

namespace BudgetManager.Application.Wallets;

public record AddWalletCommand(string Name, string IconUrl, WalletType Type, Guid UserId) : ICommand<Wallet>;

internal sealed class AddWalletCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<AddWalletCommand, Wallet>
{
    public Task<Wallet> Handle(AddWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = new Wallet(request.Name, request.IconUrl, request.Type, new UserId(request.UserId));
        db.Wallets.Add(wallet);
        return Task.FromResult(wallet);
    }
}
