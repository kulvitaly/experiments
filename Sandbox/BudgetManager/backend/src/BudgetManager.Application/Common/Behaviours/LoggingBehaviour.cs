using System.Diagnostics;
using BudgetManager.Application.Common.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BudgetManager.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(
    ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private const int SlowRequestThresholdMs = 500;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).GetFriendlyName();

        logger.LogDebug("Handling {Name}", requestName);

        var start = Stopwatch.GetTimestamp();
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            var elapsed = Stopwatch.GetElapsedTime(start);
            logger.LogError(ex,
                "Unhandled exception while handling {Name} after {Elapsed} ms",
                requestName, (long)elapsed.TotalMilliseconds);

            throw;
        }
        finally
        {
            var elapsed = Stopwatch.GetElapsedTime(start);

            if (elapsed.TotalMilliseconds > SlowRequestThresholdMs)
            {
                logger.LogWarning("Slow request {Name} completed in {Elapsed} ms",
                    requestName, (long)elapsed.TotalMilliseconds);
            }
            else
            {
                logger.LogTrace("{Name} completed in {Elapsed} ms",
                    requestName, (long)elapsed.TotalMilliseconds);
            }
        }
    }
}
