using System.ComponentModel;

namespace Model.Types.Common;

public abstract record Measure(string Unit);

public record DiscreteMeasure(string Unit, uint Value) : Measure(Unit);

public record ContinuousMeasure(string Unit, decimal Value) : Measure(Unit);

public static class MeasureExtensions
{
    public static Measure AsDiscriminatedUnion(this Measure m) 
        => m switch
        {
            DiscreteMeasure or ContinuousMeasure => m,
            _ => throw new InvalidOperationException($"Unknown measure type: {m.GetType().Name}")
        };

    public static TResult MapAny<TResult>(this Measure m, 
        Func<DiscreteMeasure, TResult> discrete,
        Func<ContinuousMeasure, TResult> continuous)
            => m.AsDiscriminatedUnion() switch
            {
                DiscreteMeasure dm => discrete(dm),
                ContinuousMeasure cm => continuous(cm),
                _ => default!
            };
}