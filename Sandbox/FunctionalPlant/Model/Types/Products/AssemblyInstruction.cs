using System.Collections.Immutable;
using Model.Types.Common;
using Models.Types;

namespace Model.Types.Products;

public class AssemblyInstruction
{
    private ImmutableList<InstructionSegment> Segments { get; }

    public AssemblyInstruction()
        : this(ImmutableList<InstructionSegment>.Empty) 
    {}

    public AssemblyInstruction(IEnumerable<InstructionSegment> segments)
        : this(segments.ToImmutableList())
    {}

    private AssemblyInstruction(ImmutableList<InstructionSegment> segments)
        => Segments = segments;

    public IEnumerable<(Part part, DiscreteMeasure quantity)> Components
        => Segments.OfType<NewPartSegment>().Select(s => (s.Part, s.Quantity));

    public AssemblyInstruction Append(params InstructionSegment[] segments)
        => segments .Length == 0 ? this : new(Segments.AddRange(segments));
    
public AssemblyInstruction Append(IEnumerable<InstructionSegment> segments)
        => new(Segments.AddRange(segments));
}