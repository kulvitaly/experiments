using Application.Persistence;
using Model.Common;
using Model.Types.Common;
using Models.Types;

namespace TestPersistence;

public class InventoryRepository : IReadOnlyRepository<(Part part, DiscreteMeasure quantity)>
{
    private PartsReadRepository Parts { get; } = new();
    private Random Random { get; } = new(42);

    public Option<(Part part, DiscreteMeasure quantity)> Find(Guid id)
        => Parts.Find(id).Filter(Exists).Map(part => (part, QuantityFor(part)));

    private DiscreteMeasure QuantityFor(Part part)
        => new("Piece", Exists(part) ? (uint)Random.Next(9, 17) : 0);

    public IEnumerable<(Part part, DiscreteMeasure quantity)> GetAll()
        => Parts.GetAll().Where(Exists).Select(part => (part, QuantityFor(part)));

    private bool Exists(Part part)
        => true; //part.Sku.Value[part.Sku.Value.Length / 2] % 5 > 1;

}