using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Families;
using BudgetManager.Domain.Users;
using BudgetManager.Domain.Wallets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Wallets;

public record GetWalletsQuery(FamilyId FamilyId, UserId? UserId) : IQuery<IReadOnlyList<Wallet>>;

internal sealed class GetWalletsQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetWalletsQuery, IReadOnlyList<Wallet>>
{
    public async Task<IReadOnlyList<Wallet>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
    {
        var query =
            from wallet in db.Wallets
            join user in db.Users on wallet.OwnerId equals user.Id
            where user.FamilyId == request.FamilyId
            select wallet;

        if (request.UserId is not null)
        {
            query = query.Where(w => w.OwnerId == request.UserId);
        }

        return await query.ToListAsync(cancellationToken);
    }
}
