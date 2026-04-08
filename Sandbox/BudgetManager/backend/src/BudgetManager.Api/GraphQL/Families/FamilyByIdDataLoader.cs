using BudgetManager.Application.Families;
using BudgetManager.Domain.Families;
using GreenDonut;
using MediatR;

namespace BudgetManager.Api.GraphQL.Families;

public sealed class FamilyByIdDataLoader(
    IMediator mediator,
    IBatchScheduler batchScheduler,
    DataLoaderOptions options)
    : BatchDataLoader<Guid, Family>(batchScheduler, options)
{
    protected override async Task<IReadOnlyDictionary<Guid, Family>> LoadBatchAsync(
        IReadOnlyList<Guid> keys,
        CancellationToken cancellationToken)
    {
        var familyIds = keys.Select(k => new FamilyId(k)).ToList();

        var families = await mediator.Send(
            new GetFamiliesByIdsQuery(familyIds),
            cancellationToken);

        return families.ToDictionary(f => f.Id.Value);
    }
}
