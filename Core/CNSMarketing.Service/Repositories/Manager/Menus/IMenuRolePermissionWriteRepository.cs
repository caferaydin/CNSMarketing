using CNSMarketing.Application.Repositories;
using CNSMarketing.Domain.Entities;

namespace CNSMarketing.Application.Repositories
{
    public interface IMenuRolePermissionWriteRepository : IWriteRepository<MenuRolePermission, Guid>
    {
        Task<bool> SetPermissionsByRoleIdAsync(string roleId, IEnumerable<Guid> permissionIds);
    }
}
