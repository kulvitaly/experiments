using BudgetManager.Api.GraphQL.Categories;
using BudgetManager.Api.GraphQL.Families;
using BudgetManager.Application.Categories;
using BudgetManager.Application.Families;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using MediatR;

namespace BudgetManager.Api.GraphQL;

public class Query
{
    [GraphQLType(typeof(ListType<CategoryObjectType>))]
    public Task<IReadOnlyList<Category>> IncomeCategories(
        Guid familyId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetIncomeCategoriesQuery(new FamilyId(familyId)), cancellationToken);

    [GraphQLType(typeof(ListType<CategoryObjectType>))]
    public Task<IReadOnlyList<Category>> ExpenseCategories(
        Guid familyId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetExpenseCategoriesQuery(new FamilyId(familyId)), cancellationToken);

    [GraphQLType(typeof(ListType<FamilyObjectType>))]
    public Task<IReadOnlyList<Family>> Families(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new GetFamiliesQuery(), cancellationToken);
}
