using Domain.CaseAktifAggregate;
using Domain.IRepositoryAggregate;

namespace Repository.RepositoryAggregate
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CaseAktifDbContext context) : base(context)
        {

        }
    }
}
