using BudgetManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BudgetManager.Application.Common.Behaviours;

public class TransactionBehaviour<TRequest, TResponse>(
    IUnitOfWork unitOfWork,
    ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogDebug("SaveChangesAsync committed for {Name}", typeof(TRequest).Name);
        return response;
    }
}
