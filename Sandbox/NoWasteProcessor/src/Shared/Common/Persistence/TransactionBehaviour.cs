using System.Diagnostics;
using Application.Common.MediatR;
using Application.Common.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MOM360.Application.Common.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, ICommand<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TRequest> _logger;
    //private readonly ICurrentUserService _currentUserService;
    //private readonly IIdentityService _identityService;

    public TransactionBehaviour(
        IUnitOfWork unitOfWork,
        ILogger<TRequest> logger)
        //ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        //_currentUserService = currentUserService;
        // _identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        await _unitOfWork.SaveChanges(cancellationToken);

        return response;
    }
}
