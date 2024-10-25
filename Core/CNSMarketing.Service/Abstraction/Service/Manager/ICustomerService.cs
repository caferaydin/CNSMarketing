using CNSMarketing.Domain.Entity.Manager;
using CNSMarketing.Service.Repositories.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNSMarketing.Service.Abstraction.Service.Manager
{
    public interface ICustomerService : IGenericService<Customer, int, ICustomerReadRepository, ICustomerWriteRepository>
    {
    }
}
