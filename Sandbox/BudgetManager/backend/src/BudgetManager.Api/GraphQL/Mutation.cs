using BudgetManager.Api.GraphQL.Categories;
using BudgetManager.Api.GraphQL.Families;
using BudgetManager.Api.GraphQL.Users;
using BudgetManager.Application.Categories;
using BudgetManager.Application.Families;
using BudgetManager.Application.Users;
using BudgetManager.Domain.Categories;
using BudgetManager.Domain.Families;
using BudgetManager.Domain.Users;
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

    [GraphQLType(typeof(UserObjectType))]
    public Task<User> CreateUser(
        string name,
        string iconUrl,
        Guid? familyId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new CreateUserCommand(name, iconUrl, familyId), cancellationToken);

    [GraphQLType(typeof(UserObjectType))]
    public Task<User> UpdateUser(
        Guid id,
        string name,
        string iconUrl,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new UpdateUserCommand(id, name, iconUrl), cancellationToken);

    public Task<bool> DeleteUser(
        Guid id,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
        => mediator.Send(new DeleteUserCommand(id), cancellationToken);
}
