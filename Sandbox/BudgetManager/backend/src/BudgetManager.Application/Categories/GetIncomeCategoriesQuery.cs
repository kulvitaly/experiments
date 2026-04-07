using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Categories;

public record GetIncomeCategoriesQuery(FamilyId FamilyId) : IQuery<IReadOnlyList<Category>>;

internal sealed class GetIncomeCategoriesQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetIncomeCategoriesQuery, IReadOnlyList<Category>>
{
    public async Task<IReadOnlyList<Category>> Handle(
        GetIncomeCategoriesQuery request, CancellationToken cancellationToken)
        => await db.Categories
            .Where(c => c.FamilyId == request.FamilyId && c.Type == CategoryType.Income)
            .ToListAsync(cancellationToken);
}
