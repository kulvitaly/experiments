using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace RealEstateApi.Controllers;

[Route("[controller]")]
public class GraphQlController : Controller
{
    private readonly ISchema _schema;
    private readonly IDocumentExecuter _documentExecuter;

    public GraphQlController(ISchema schema, IDocumentExecuter documentExecuter)
    {
        _schema = schema;
        _documentExecuter = documentExecuter;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GraphQlQuery query, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(query.Query, nameof(query.Query));

        var executionOptions = new ExecutionOptions
        {
            Schema = _schema,
            Query = query.Query,
            OperationName = query.OperationName,
            Variables = query.Variables
        };

        var result = await _documentExecuter.ExecuteAsync(executionOptions);

        if (result.Errors?.Count > 0)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
