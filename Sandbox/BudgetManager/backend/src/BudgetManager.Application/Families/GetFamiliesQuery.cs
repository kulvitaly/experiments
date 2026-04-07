using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Families;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Families;

public record GetFamiliesQuery : IQuery<IReadOnlyList<Family>>;

internal sealed class GetFamiliesQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetFamiliesQuery, IReadOnlyList<Family>>
{
    public async Task<IReadOnlyList<Family>> Handle(
        GetFamiliesQuery request, CancellationToken cancellationToken)
        => await db.Families.ToListAsync(cancellationToken);
}
