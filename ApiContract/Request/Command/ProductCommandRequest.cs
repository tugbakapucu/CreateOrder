using ApiContract.Response.Command;

namespace ApiContract.Request.Command
{
    public class ProductCommandRequest : RequestBase<ProductCommandResponse>
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
