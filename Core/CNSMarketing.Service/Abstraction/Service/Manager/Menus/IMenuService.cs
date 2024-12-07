using CNSMarketing.Application.Repositories;
using CNSMarketing.Domain.Entities;

namespace CNSMarketing.Application.Abstraction.Service.Manager
{
    public interface IMenuService : IGenericService<Menu,Guid, IMenuReadRepository, IMenuWriteRepository>
    {
        public Task<IEnumerable<Menu>> GetMenusAsync();



    }
}
