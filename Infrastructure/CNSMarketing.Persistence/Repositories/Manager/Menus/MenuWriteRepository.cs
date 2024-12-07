using CNSMarketing.Application.Repositories;
using CNSMarketing.Domain.Entities;
using CNSMarketing.Persistence.Context;

namespace CNSMarketing.Persistence.Repositories.Manager
{
    public class MenuWriteRepository : WriteRepository<Menu, Guid>, IMenuWriteRepository
    {
        public MenuWriteRepository(CNSMarketingDbContext context) : base(context)
        {
        }
    }
}
