using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Model.Types.Common;
using Models.Types;
using Models.Types.Common;

namespace Model.Types.Products;

public class AssemblySpecification
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; } = string.Empty;
    public required string Description {get; init; } = string.Empty;

    private ImmutableList<AssemblyInstruction> InstructionsList { get; init; } = ImmutableList<AssemblyInstruction>.Empty;
    public IEnumerable<AssemblyInstruction> Instructions => InstructionsList;

    public IEnumerable<(Part part, DiscreteMeasure quantity)> Components
    => InstructionsList.SelectMany(i => i.Components)
        .GroupBy(row => (part: row.part, qunit: row.quantity.Unit), row => row.quantity)
        .Select(group => (group.Key.part, group.Sum()));
        
    public IEnumerable<(InventoryItem item, Measure quantity)> Consumables { get; init; } = Enumerable.Empty<(InventoryItem, Measure)>(); 

    public AssemblySpecification(Guid id) => Id = id;

    [SetsRequiredMembers]
    public AssemblySpecification(AssemblySpecification other)
        => (Id, Name, Description, InstructionsList) = (other.Id, other.Name, other.Description, other.InstructionsList);    

    public AssemblySpecification Add(params AssemblyInstruction[] instructions)
        => new(this) { InstructionsList = InstructionsList.AddRange(instructions) };
}