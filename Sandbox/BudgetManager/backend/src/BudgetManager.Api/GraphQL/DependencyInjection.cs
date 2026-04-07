using BudgetManager.Api.GraphQL.Categories;
using BudgetManager.Api.GraphQL.Families;
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
            .AddType<FamilyObjectType>();

        return services;
    }
}
