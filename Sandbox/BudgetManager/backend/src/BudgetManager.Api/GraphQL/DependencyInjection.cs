using BudgetManager.Api.GraphQL.Categories;
using BudgetManager.Api.GraphQL.Families;
using BudgetManager.Api.GraphQL.Users;
using BudgetManager.Api.GraphQL.Wallets;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Api.GraphQL;

public static class DependencyInjection
{
    public static IServiceCollection AddGraphQl(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddType<CategoryObjectType>()
            .AddType<FamilyObjectType>()
            .AddType<UserObjectType>()
            .AddType<WalletObjectType>()
            .AddDataLoader<FamilyByIdDataLoader>()
            .AddDataLoader<UserByIdDataLoader>()
            .AddDataLoader<WalletByIdDataLoader>();

        return services;
    }
}
