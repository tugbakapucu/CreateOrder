using ApiContract.Response;
using MediatR;

namespace ApiContract.Request
{
    public abstract class RequestBase<TResponse> : IRequest<ResponseBase<TResponse>>
    {
    }
}
