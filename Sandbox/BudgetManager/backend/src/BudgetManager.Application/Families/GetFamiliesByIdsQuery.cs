using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Families;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Families;

public record GetFamiliesByIdsQuery(IReadOnlyCollection<FamilyId> Ids)
    : IQuery<IReadOnlyList<Family>>;

internal sealed class GetFamiliesByIdsQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetFamiliesByIdsQuery, IReadOnlyList<Family>>
{
    public async Task<IReadOnlyList<Family>> Handle(
        GetFamiliesByIdsQuery request, CancellationToken cancellationToken)
        => await db.Families
            .Where(f => request.Ids.Contains(f.Id))
            .ToListAsync(cancellationToken);
}
