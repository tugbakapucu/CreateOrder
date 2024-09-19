using ApiContract.Request.Command;
using ApiContract.Response;
using ApiContract.Response.Command;
using ApplicationService.Handler.Services;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Api.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly CaseAktifDbContext _context;
        private readonly IProductService _productService;
        private readonly IMediator _mediator;

        public ProductController(CaseAktifDbContext context, IProductService productService, IMediator mediator)
        {
            _mediator = mediator;
            _productService = productService;
            _context = context;
        }

        [HttpGet("AllProducts")]
        public async Task<IActionResult> AllProducts()
        {
            //Redisdeki datayı direk okuduk.
            var result =  await _productService.GetProductsAsync();
            return Ok(result);
        }

        //IMediator ile bir tane örnek ürün ekleme
        //Validation eklemesi ApiContract/ Validation altındadir.
        [HttpPost("[action]")]
        [ProducesResponseType(200, Type = typeof(ResponseBase<ProductCommandResponse>))]
        public async Task<IActionResult> InsertProduct([FromBody] ProductCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
