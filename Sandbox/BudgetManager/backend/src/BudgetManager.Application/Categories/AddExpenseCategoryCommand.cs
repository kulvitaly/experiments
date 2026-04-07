using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using MediatR;

namespace BudgetManager.Application.Categories;

public record AddExpenseCategoryCommand(string Name, string IconUrl, FamilyId FamilyId) : ICommand<Category>;

internal sealed class AddExpenseCategoryCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<AddExpenseCategoryCommand, Category>
{
    public Task<Category> Handle(AddExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name, request.IconUrl, CategoryType.Expense, request.FamilyId);
        db.Categories.Add(category);
        return Task.FromResult(category);
    }
}
