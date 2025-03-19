using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace KingPriceDemo.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
                                (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
                                : IPipelineBehavior<TRequest, TResponse>
                                where TRequest : notnull, IRequest<TResponse>
                                where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;

            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
            {
                logger.LogWarning(
                    "[PERFORMANCE] The request {Request} took {TimeTaken} seconds. with data = {RequestData}",
                    typeof(TRequest).Name,
                    timeTaken.Seconds,
                    request
                );
            }

            return response;
        }
    }
}
