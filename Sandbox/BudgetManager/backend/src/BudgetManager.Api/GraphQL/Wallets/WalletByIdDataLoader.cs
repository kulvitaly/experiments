using BudgetManager.Application.Wallets;
using BudgetManager.Domain.Wallets;
using GreenDonut;
using MediatR;

namespace BudgetManager.Api.GraphQL.Wallets;

public sealed class WalletByIdDataLoader(
    IMediator mediator,
    IBatchScheduler batchScheduler,
    DataLoaderOptions options)
    : BatchDataLoader<Guid, Wallet>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, Wallet>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var walletIds = keys.Select(k => new WalletId(k)).ToList();

        var wallets = await mediator.Send(
            new GetWalletsByIdsQuery(walletIds),
            cancellationToken);

        return wallets.ToDictionary(w => w.Id.Value);
    }
}
