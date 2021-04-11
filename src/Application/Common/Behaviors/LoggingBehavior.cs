using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest>
       : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _user;

        public LoggingBehavior(
            ILogger<TRequest> logger,
            ICurrentUserService user)
        {
            _logger = logger;
            _user = user;
        }

        public async Task Process(
            TRequest request,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation($"Application Request: {requestName} {_user.UserId} {request}");
            await Task.FromResult(0);
        }
    }
}
