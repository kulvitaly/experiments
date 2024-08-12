
using GraphQL;

namespace Utilities;

public class GraphQlQuery
{
    public string OperationName { get; set; } = null!;
    public string Query { get; set; } = null!;
    public Inputs? Variables { get; set; }
}
