using Domain.CaseAktifAggregate;

namespace ApplicationService.Handler.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
    }
}
