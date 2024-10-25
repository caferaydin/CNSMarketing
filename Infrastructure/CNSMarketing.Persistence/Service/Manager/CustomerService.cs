using CNSMarketing.Domain.Entity.Manager;
using CNSMarketing.Service.Abstraction.Service.Manager;
using CNSMarketing.Service.Repositories.Manager;

namespace CNSMarketing.Persistence.Service.Manager
{
    public class CustomerService : GenericService<Customer, int, ICustomerReadRepository, ICustomerWriteRepository>, ICustomerService
    {
        public CustomerService(ICustomerReadRepository readRepository, ICustomerWriteRepository writeRepository) : base(readRepository, writeRepository)
        {
        }

    }
}
