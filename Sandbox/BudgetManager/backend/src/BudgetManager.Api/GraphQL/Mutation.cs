using BudgetManager.Api.GraphQL.Categories;
using BudgetManager.Api.GraphQL.Families;
using BudgetManager.Application.Categories;
using BudgetManager.Application.Families;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using MediatR;

namespace BudgetManager.Api.GraphQL;

public class Mutation
{
    [GraphQLType(typeof(CategoryObjectType))]
    public Task<Category> AddIncomeCategory(
        string name,
        string iconUrl,
        Guid familyId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new AddIncomeCategoryCommand(name, iconUrl, new FamilyId(familyId)), cancellationToken);

    [GraphQLType(typeof(CategoryObjectType))]
    public Task<Category> AddExpenseCategory(
        string name,
        string iconUrl,
        Guid familyId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new AddExpenseCategoryCommand(name, iconUrl, new FamilyId(familyId)), cancellationToken);

    [GraphQLType(typeof(FamilyObjectType))]
    public Task<Family> RegisterFamily(
        string name,
        string? iconUrl,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new RegisterFamilyCommand(name, iconUrl), cancellationToken);
}
