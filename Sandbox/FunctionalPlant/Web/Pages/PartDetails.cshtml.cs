using Application.Persistence;
using Models.Types;
using Models.Media;
using Models.Types.Media;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model.Media.Types;
using Web.Configuration;
using Model.Common;

namespace Web.Pages;


public class PartDetailsModel : PageModel
{
    IReadOnlyRepository<Part> Parts { get; }
    public BarcodeGeneratorFactory BarcodeGeneratorFactory { get; }

    public PartDetailsModel(IReadOnlyRepository<Part> parts, BarcodeGeneratorFactory barcodeGeneratorFactory)
    {
        Parts = parts;
        GenerateBarcode = barcodeGeneratorFactory.Print;
    }
    
    public Part Part { get; set; } = null!;
    public FileContent BarcodeImage {get; set; } = null!;

    public IActionResult OnGet(Guid id)
    {
        return Parts.Find(id)
            .Map(part =>
            {
                Part = part;
                BarcodeImage = GenerateBarcode(Part.Sku);
                return (IActionResult)Page();
            })
            .Reduce(NotFound);
    }

    private BarcodeGenerator GenerateBarcode { get; }
}