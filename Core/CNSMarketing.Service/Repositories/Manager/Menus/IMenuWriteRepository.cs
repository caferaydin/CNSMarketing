using CNSMarketing.Domain.Entities;
using CNSMarketing.Application.Repositories;

namespace CNSMarketing.Application.Repositories
{
    public interface IMenuWriteRepository : IWriteRepository<Menu,Guid>
    {
    }
}
