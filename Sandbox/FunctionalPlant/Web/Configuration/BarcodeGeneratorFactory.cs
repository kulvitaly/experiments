using Microsoft.Extensions.Options;
using Model.Media.Types;
using Models.Media;

namespace Web.Configuration;

public class BarcodeGeneratorFactory
{
    private IDictionary<string, BarcodeGenerator> Generators { get; }

    public BarcodeGeneratorFactory(IOptions<BarcodeFormatOptions> options)
    {
        Generators = options.Value.Formats.ToDictionary(
            format => format.Name,
            format => CreateBarcodeGenerator(format));
    }

    public BarcodeGenerator Inline => GetGenerator(BarcodeFormatOptions.Inline);
    public BarcodeGenerator Print => GetGenerator(BarcodeFormatOptions.Print);

    public BarcodeGenerator this[string name] => GetGenerator(name);

    private static BarcodeGenerator CreateBarcodeGenerator(BarcodeFormat format) 
        => Code39Generator.ToCode39.Apply(format.Margins, format.Style);

    private static BarcodeMargins DefaultMargins => new(0, 0, 20);
    private static Code39Style DefaultStyle => new(1, 3, 1, 1, true);

    private static BarcodeGenerator DefaultBarcodeGenerator => 
        Code39Generator.ToCode39.Apply(DefaultMargins, DefaultStyle); 

    private BarcodeGenerator GetGenerator(string format) 
        => Generators.TryGetValue(format, out var generator) 
            ? generator 
            : DefaultBarcodeGenerator; 
}