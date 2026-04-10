using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Users;

public record GetUsersByIdsQuery(IReadOnlyCollection<UserId> Ids) : IQuery<IReadOnlyList<User>>;

internal sealed class GetUsersByIdsQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetUsersByIdsQuery, IReadOnlyList<User>>
{
    public async Task<IReadOnlyList<User>> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
        => await db.Users
            .Where(u => request.Ids.Contains(u.Id))
            .ToListAsync(cancellationToken);
}
