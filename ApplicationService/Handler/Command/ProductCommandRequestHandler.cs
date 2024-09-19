using ApiContract.Request.Command;
using ApiContract.Response;
using ApiContract.Response.Command;
using Domain;
using Domain.IRepositoryAggregate;

namespace ApplicationService.Handler.Command
{
    public class ProductCommandRequestHandler : RequestHandlerBase<ProductCommandRequest, ProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDbContextHandler _dbContextHandler;

        public ProductCommandRequestHandler(IProductRepository productRepository, IDbContextHandler dbContextHandler)
        {
            _productRepository = productRepository;
            _dbContextHandler = dbContextHandler;
        }
        public override async Task<ResponseBase<ProductCommandResponse>> Handle(ProductCommandRequest request, CancellationToken cancellationToken)
        {
           //Burada ürün varmı yok mu kontrolu yapılablir sonrasında db ye eklenebilir.
            return new ResponseBase<ProductCommandResponse> { Data = new ProductCommandResponse { Id = 1}, Message = "", MessageCode = 200, Success = true };
        }
    }
}
