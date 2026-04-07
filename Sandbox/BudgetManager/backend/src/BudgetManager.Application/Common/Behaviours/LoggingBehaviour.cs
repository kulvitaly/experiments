using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace BudgetManager.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<LoggingBehaviour<TRequest>> logger)
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Request: {Name} {@Request}", typeof(TRequest).Name, request);
        return Task.CompletedTask;
    }
}
