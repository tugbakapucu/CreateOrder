using ApiContract.Response;
using MediatR;
using System.Diagnostics;

namespace Container.Decorator
{
    public class LoggingHandler<TRequest, TResponse> : DecoratorBase<TRequest, TResponse>
        where TResponse : ResponseBase<TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IAppLogger _appLogger;

        public LoggingHandler(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }
        public override async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)

        {
            var handlerMethodInfo = GetHandlerMethodInfo();
            try
            {
                _appLogger.MethodEntry(request, handlerMethodInfo);
            }
            catch
            {
            }


            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            try
            {
                _appLogger.MethodExit(response, handlerMethodInfo, timer.Elapsed.TotalMilliseconds, response.MessageCode);
            }
            catch
            {
            }

            return response;
        }
    }
}
