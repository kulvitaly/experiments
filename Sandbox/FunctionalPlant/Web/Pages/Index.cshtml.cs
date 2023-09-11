using Application.Persistence;
using Models.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model.Media.Types;
using Web.Configuration;
using Models.Media;
using Model.Types.Products;
using Model.Types.Common;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private IReadOnlyRepository<(Part part, DiscreteMeasure quantity)> Inventory { get; }

    private IReadOnlyRepository<Part> Parts { get; }

    private IReadOnlyRepository<AssemblySpecification> Specifications { get; }

    public bool IsShowingAllSpecs { get; set; }

    public IndexModel(IReadOnlyRepository<Part> parts,
     IReadOnlyRepository<AssemblySpecification> specs, 
     IReadOnlyRepository<(Part part, DiscreteMeasure quantity)> inventory,
     BarcodeGeneratorFactory barcodeGeneratorFactory)
    {
        Parts = parts;
        Specifications = specs;
        Inventory = inventory;
        BarcodeGenerator = barcodeGeneratorFactory.Inline;
    }

    public BarcodeGenerator BarcodeGenerator { get; }

    public IEnumerable<Part> AllParts {get; set; } = Enumerable.Empty<Part>();

    public IEnumerable<AssemblySpecification> AllProducts {get; set; } = Enumerable.Empty<AssemblySpecification>();
    

    public void OnGet(string show)
    {
        this.IsShowingAllSpecs = show == "all";
        Func<AssemblySpecification, bool> isSupported = this.IsSupportedStrategy(show);
        AllParts = Parts.GetAll().ToList();
        AllProducts = Specifications.GetAll().Where(isSupported);
    }

    private Func<AssemblySpecification, bool> IsSupportedStrategy(string show) =>
        show == "all" ? this.AllSupported : this.IsSupportedSupply;

    private bool AllSupported(AssemblySpecification spec) => true;
    
    private bool IsSupportedSupply(AssemblySpecification spec) =>
        spec.Components.All(component => this.InSupply(component.part, component.quantity));

    private bool InSupply(Part part, DiscreteMeasure required) =>
        this.Inventory.GetAll()
            .Any(item =>
                item.part.Id == part.Id &&
                item.quantity.Unit == required.Unit &&
                item.quantity.Value >= required.Value);
    
}
