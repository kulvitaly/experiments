using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Wallets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Wallets;

public record GetWalletsByIdsQuery(IReadOnlyCollection<WalletId> Ids) : IQuery<IReadOnlyList<Wallet>>;

internal sealed class GetWalletsByIdsQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetWalletsByIdsQuery, IReadOnlyList<Wallet>>
{
    public async Task<IReadOnlyList<Wallet>> Handle(GetWalletsByIdsQuery request, CancellationToken cancellationToken)
        => await db.Wallets
            .Where(w => request.Ids.Contains(w.Id))
            .ToListAsync(cancellationToken);
}
