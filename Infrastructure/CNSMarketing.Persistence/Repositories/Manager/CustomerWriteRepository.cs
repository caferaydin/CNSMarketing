using CNSMarketing.Domain.Entity.Manager;
using CNSMarketing.Persistence.Context;
using CNSMarketing.Service.Repositories.Manager;

namespace CNSMarketing.Persistence.Repositories.Manager
{
    public class CustomerWriteRepository : WriteRepository<Customer, int>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(CNSMarketingDbContext context) : base(context)
        {
        }
    }
}
