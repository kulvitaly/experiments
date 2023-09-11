using Model.Common;
using Model.Types.Common;
using Models.Types;

namespace Model.Types.Products;

public abstract record InstructionSegment();

public record TextSegment(string Value) : InstructionSegment;

public abstract record PartSegment(Part Part, DiscreteMeasure Quantity) : InstructionSegment
{
    public Option<DiscreteMeasure> NonUnitQuantity => Quantity.Optional().Filter(q => q.Value > 1);
}

public record NewPartSegment(Part part, DiscreteMeasure Quantity) : PartSegment(part, Quantity);

public record PartReferenceSegment(Part part, DiscreteMeasure Quantity) : PartSegment(part, Quantity);
