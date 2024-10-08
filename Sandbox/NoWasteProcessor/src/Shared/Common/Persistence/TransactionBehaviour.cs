using Application.Common.MediatR;
using Application.Common.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Persistence;

public class TransactionBehaviour<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TRequest> _logger;

    public TransactionBehaviour(
        IUnitOfWork unitOfWork,
        ILogger<TRequest> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        var type = request.GetType();
        if (type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICommand<>)))
        {
            await _unitOfWork.SaveChanges(cancellationToken);
            _logger.LogDebug("Save changes");
        }

        return response;
    }
}
