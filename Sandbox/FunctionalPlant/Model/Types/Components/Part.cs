using Model.Types.Common;

namespace Models.Types;

public abstract record InventoryItem(Guid Id, string Name, StockKeepingUnit Sku);

public record Part(Guid Id, string Name, StockKeepingUnit Sku)
    : InventoryItem(Id, Name, Sku);

public record Material(Guid Id, string Name, StockKeepingUnit Sku, ContinuousMeasure Quantity)
    : InventoryItem(Id, Name, Sku);

public static class InventoryItemExtensions
{
    public static InventoryItem AsDiscriminatedUnion(this InventoryItem item) 
        => item switch
        {
            Part or Material => item,
            _ => throw new InvalidOperationException($"Unknown inventory item type: {item.GetType().Name}")
        };

    public static TResult MapAny<TResult>(this InventoryItem item,
        Func<Part, TResult> part,
        Func<Material, TResult> material)
            => item.AsDiscriminatedUnion() switch
            {
                Part p => part(p),
                Material m => material(m),
                _ => default!
            };
}