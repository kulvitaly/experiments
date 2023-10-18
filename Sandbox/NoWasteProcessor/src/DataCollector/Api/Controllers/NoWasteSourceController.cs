using Application.RawContent.Commands;
using Application.RawContent.GetLatest;
using Common.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProducerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NoWasteSourceController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<NoWasteSourceController> _logger;

    public NoWasteSourceController(ISender sender, ILogger<NoWasteSourceController> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] string content, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received message: {Html}", content);

        await _sender.Send(new AddRawContentMessageCommand(content), cancellationToken);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpGet("latest")]
    public async Task<ActionResult<string>> GetLatest(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetLatestContentQuery(), cancellationToken);
        return result.Map(content => (ActionResult)Ok(content.Content))
            .Reduce(NoContent());
    }
}