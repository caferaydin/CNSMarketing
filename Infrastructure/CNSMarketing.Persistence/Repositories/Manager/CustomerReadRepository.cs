using CNSMarketing.Domain.Entity.Manager;
using CNSMarketing.Persistence.Context;
using CNSMarketing.Application.Repositories.Manager;

namespace CNSMarketing.Persistence.Repositories.Manager
{
    public class CustomerReadRepository : ReadRepository<Customer, int>, ICustomerReadRepository
    {
        public CustomerReadRepository(CNSMarketingDbContext context) : base(context)
        {
        }
    }
}
