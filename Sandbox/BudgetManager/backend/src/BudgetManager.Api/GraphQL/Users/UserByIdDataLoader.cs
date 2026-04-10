using BudgetManager.Application.Users;
using BudgetManager.Domain.Users;
using GreenDonut;
using MediatR;

namespace BudgetManager.Api.GraphQL.Users;

public sealed class UserByIdDataLoader(
    IMediator mediator,
    IBatchScheduler batchScheduler,
    DataLoaderOptions options)
    : BatchDataLoader<Guid, User>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, User>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var userIds = keys.Select(k => new UserId(k)).ToList();

        var users = await mediator.Send(
            new GetUsersByIdsQuery(userIds),
            cancellationToken);

        return users.ToDictionary(u => u.Id.Value);
    }
}
