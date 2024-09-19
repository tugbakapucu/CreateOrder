using ApiContract.Request;
using ApiContract.Response;
using MediatR;

namespace ApplicationService.Handler
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, ResponseBase<TResponse>>
          where TRequest : RequestBase<TResponse>
    {
        public abstract Task<ResponseBase<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
