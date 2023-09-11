using Models.Types;
using Models.Types.Media;
using Models.Common;
using SkiaSharp;
using Model.Media.Types;

namespace Models.Media;

public static class Code39Generator
{
    // public static FileContent ToCode39(this StockKeepingUnit sku, int barHeight)
    //     => sku.Value.ToCode39Bars().ToCode39Bitmap(barHeight).ToPng();

    public static BarcodeGeneratorEx ToCode39 => (margins, style, sku) =>
        sku.Value.ToCode39Bars().ToCode39Bitmap(margins, style).ToPng();

    private static SKBitmap ToCode39Bitmap(this IEnumerable<int> bars, BarcodeMargins margins, Code39Style style)
        => bars.ToGraphicalLines(style).ToBarcodeBitmap(margins, style);

    private static SKPaint[] ToGraphicalLines(this IEnumerable<int> bars, Code39Style style)
        => bars.ToGraphicalLines(Gap(style), ThinBar(style), ThickBar(style));

    private static SKPaint[] ToGraphicalLines(this IEnumerable<int> bars, params SKPaint[] lines)
        => bars.Select(bar => lines[bar]).ToArray();
    
    private static SKPaint ThickBar(Code39Style style) =>
        Bar(SKColors.Black, style.ThickBarWidth, style.Antialias);

   private static SKPaint ThinBar(Code39Style style) =>
        Bar(SKColors.Black, style.ThinBarWidth, style.Antialias);

    private static SKPaint Gap(Code39Style style) =>
        Bar(SKColors.Transparent, style.GapWidth, style.Antialias);

    private static SKPaint Bar(SKColor color, float thickness, bool antialias) => new()
    {
        Color = color,
        Style = SKPaintStyle.Stroke,
        StrokeCap = SKStrokeCap.Butt,
        StrokeWidth = thickness,
        IsAntialias = antialias,
    };

    private static SKBitmap ToBarcodeBitmap(this SKPaint[] bars, BarcodeMargins margins, Code39Style style)
    {
        float barsWidth = bars.Sum(line => line.StrokeWidth);
        float height = margins.BarHeight + 2 * margins.Vertical;
        float width = barsWidth + (bars.Length - 1) * style.Padding + 2 * margins.Horizontal;

        SKBitmap bitmap = InitializeBitmap(width, height);
        SKCanvas canvas = new(bitmap);

        float offset = margins.Horizontal;
        foreach (var bar in bars)
        {
            float x = offset + bar.StrokeWidth / 2;
            canvas.DrawLine(x, margins.Vertical, x, margins.BarHeight + margins.Vertical, bar);
            offset += bar.StrokeWidth + style.Padding;
        }

        return bitmap;
    }

    private static SKBitmap InitializeBitmap(float width, float height)
    {
        SKBitmap bitmap = new((int)Math.Ceiling(width), (int)Math.Ceiling(height));
        SKCanvas canvas = new(bitmap);
        canvas.Clear(SKColors.White);
        return bitmap;
    }

    // public static FileContent ToCode39(this StockKeepingUnit sku, int barHeight)
    // {
    //     float horizontalMargin = 5;
    //     float verticalMargin = 3;
    //     float padding = 2.0f;

    //     SKPaint[] bars = new[]
    //     {
    //         ThinBar, Space, ThinBar, ThickBar, ThickBar, ThinBar,
    //         ThickBar, ThinBar, ThinBar, Space, ThickBar, ThinBar,
    //         ThinBar, ThickBar, ThinBar, Space, ThickBar, ThinBar,
    //         ThinBar, Space, ThinBar, ThickBar, ThickBar, ThinBar,
    //     };

    //     int height = (int)Math.Ceiling(barHeight + verticalMargin * 2);
    //     int width = (int)Math.Ceiling(bars.Sum(bar => bar.StrokeWidth) + (bars.Length - 1) * padding + horizontalMargin * 2);

    //     SKBitmap bitmap = new(width, height);
    //     SKCanvas canvas = new(bitmap);
    //     canvas.Clear(SKColors.White);

    //     float x = horizontalMargin;
    //     foreach (SKPaint bar in bars)
    //     {
    //         float lineX = x + bar.StrokeWidth / 2;
    //         canvas.DrawLine(lineX, verticalMargin, lineX, barHeight + verticalMargin, bar);
    //         x += bar.StrokeWidth + padding;
    //     }

    //     return bitmap.ToPng();
    // }

    // private static FileContent ToPng(this SKBitmap bitmap) =>
    //     new(bitmap.Encode(SKEncodedImageFormat.Png, 100).ToArray(), "image/png");

    // private static SKPaint ThickBar { get; } = new()
    // {
    //     Color = SKColors.Black,
    //     Style = SKPaintStyle.Stroke,
    //     StrokeCap = SKStrokeCap.Butt,
    //     StrokeWidth = 4.0f,
    //     IsAntialias = true,
    // };

    // private static SKPaint ThinBar { get; } = new()
    // {
    //     Color = SKColors.Black,
    //     Style = SKPaintStyle.Stroke,
    //     StrokeCap = SKStrokeCap.Butt,
    //     StrokeWidth = 1.5f,
    //     IsAntialias = true,
    // };

    // private static SKPaint Space { get; } = new()
    // {
    //     Color = SKColors.Transparent,
    //     Style = SKPaintStyle.Stroke,
    //     StrokeCap = SKStrokeCap.Butt,
    //     StrokeWidth = 2.0f,
    //     IsAntialias = true,
    // };
}
