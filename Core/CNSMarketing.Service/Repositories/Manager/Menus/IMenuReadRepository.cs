using CNSMarketing.Application.ViewModels;
using CNSMarketing.Application.Repositories;
using CNSMarketing.Domain.Entities;

namespace CNSMarketing.Application.Repositories
{
    public interface IMenuReadRepository : IReadRepository<Menu, Guid>
    {
        Task<List<Menu>> GetAllMenusAsync();
        Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string roleId);

    }
}
