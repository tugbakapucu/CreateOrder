using Domain.CaseAktifAggregate;
using Domain.IRepositoryAggregate;

namespace Repository.RepositoryAggregate
{
    public class CustomerOrderRepository : GenericRepository<CustomerOrder>, ICustomerOrderRepository
    {
        public CustomerOrderRepository(CaseAktifDbContext context) : base(context)
        {

        }
    }
}
