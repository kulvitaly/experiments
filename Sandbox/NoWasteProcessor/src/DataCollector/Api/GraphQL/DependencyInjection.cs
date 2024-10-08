using Api.GraphQL.Mutations;
using Api.GraphQL.Queries;
using MediatR;

namespace Api.GraphQL;

public static class DependencyInjection
{
    public static IServiceCollection AddGraphQl(this IServiceCollection services)
    {
        services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .RegisterService<ISender>();

        return services;
    }
}
