using CNSMarketing.Domain.Entity.Manager;
using CNSMarketing.Application.Repositories.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNSMarketing.Application.Abstraction.Service.Manager
{
    public interface ICustomerService : IGenericService<Customer, int, ICustomerReadRepository, ICustomerWriteRepository>
    {
    }
}
