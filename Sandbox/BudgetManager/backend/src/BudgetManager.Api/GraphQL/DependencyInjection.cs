using Microsoft.Extensions.DependencyInjection;

namespace BudgetManager.Api.GraphQL;

public static class DependencyInjection
{
    public static IServiceCollection AddGraphQl(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>();

        return services;
    }
}
