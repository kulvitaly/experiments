namespace Model.Common;

using Model.Types.Common;

public static class MeasureTransforms
{
    public static (Measure a, Measure b) SplitInHalves(this Measure m) 
        => m.MapAny(
            dm => (dm with { Value = (dm.Value + 1) / 2}, dm with { Value = dm.Value / 2}),
            cm => 
            {
                Measure half = cm with { Value = cm.Value / 2 };
                return (half, half); 
            });
        // => m.AsDiscriminatedUnion() switch
        // {
        //     DiscreteMeasure dm => SplitInHalves(dm),
        //     ContinuousMeasure cm => SplitInHalves(cm),
        //     _ => default!
        // };

    private static (Measure a, Measure b) SplitInHalves(this DiscreteMeasure dm) 
        => (dm with { Value = (dm.Value + 1) / 2}, dm with { Value = dm.Value / 2});

    private static (Measure a, Measure b) SplitInHalves(this ContinuousMeasure cm) 
    {
        Measure half = cm with { Value = cm.Value / 2 };
        return (half, half); 
    }
}