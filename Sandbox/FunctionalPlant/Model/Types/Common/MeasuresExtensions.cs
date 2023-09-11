using Model.Types.Common;

namespace Models.Types.Common;

public static class MeasuresExtensions
{
    public static DiscreteMeasure Add(this DiscreteMeasure a, DiscreteMeasure b)
    => a.Unit == b.Unit ? a with { Value = a.Value + b.Value } 
        : throw new InvalidOperationException("Cannot add measures with different units.");

    public static DiscreteMeasure Sum(this IEnumerable<DiscreteMeasure> sequence)
        => sequence.Aggregate(Add);
}