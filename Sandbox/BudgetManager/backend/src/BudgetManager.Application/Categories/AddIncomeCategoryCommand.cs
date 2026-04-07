using BudgetManager.Application.Common.Interfaces;
using BudgetManager.Application.Common.MediatR;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using MediatR;

namespace BudgetManager.Application.Categories;

public record AddIncomeCategoryCommand(string Name, string IconUrl, FamilyId FamilyId) : ICommand<Category>;

internal sealed class AddIncomeCategoryCommandHandler(IBudgetManagerDbContext db)
    : IRequestHandler<AddIncomeCategoryCommand, Category>
{
    public Task<Category> Handle(AddIncomeCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name, request.IconUrl, CategoryType.Income, request.FamilyId);
        db.Categories.Add(category);
        return Task.FromResult(category);
    }
}
