using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Common.MediatR;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    //private readonly ICurrentUserService _currentUserService;
    //private readonly IIdentityService _identityService;

    /*    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }*/

    public LoggingBehaviour(ILogger<TRequest> logger)//, ICurrentUserService currentUserService)
    {
        _logger = logger;
        //_currentUserService = currentUserService;

    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        //var userId = _currentUserService.UserId ?? string.Empty;
        string userName = string.Empty;

        //if (!string.IsNullOrEmpty(userId))
        //{
        //    userName = userId;//await _identityService.GetUserNameAsync(userId);
        //}

        _logger.LogInformation("MOM360 Request: {Name} {@Request}",
            requestName, request);
    }
}
