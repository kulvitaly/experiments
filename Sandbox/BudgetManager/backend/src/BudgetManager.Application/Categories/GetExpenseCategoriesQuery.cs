using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Application.Categories;

public record GetExpenseCategoriesQuery(FamilyId FamilyId) : IQuery<IReadOnlyList<Category>>;

internal sealed class GetExpenseCategoriesQueryHandler(IBudgetManagerDbContext db)
    : IRequestHandler<GetExpenseCategoriesQuery, IReadOnlyList<Category>>
{
    public async Task<IReadOnlyList<Category>> Handle(
        GetExpenseCategoriesQuery request, CancellationToken cancellationToken)
        => await db.Categories
            .Where(c => c.FamilyId == request.FamilyId && c.Type == CategoryType.Expense)
            .ToListAsync(cancellationToken);
}
