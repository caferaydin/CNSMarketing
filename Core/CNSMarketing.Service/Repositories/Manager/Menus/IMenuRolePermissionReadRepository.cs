using CNSMarketing.Application.ViewModels;
using CNSMarketing.Domain.Entities;
using CNSMarketing.Application.Repositories;

namespace CNSMarketing.Application.Repositories
{
    public interface IMenuRolePermissionReadRepository : IReadRepository<MenuRolePermission, Guid>
    {
        Task<List<NavigationMenuViewModel>> GetAllRolePermissionsAsync(string roleId);
        Task<List<string>> GetRoleIdsByUserId(string userId);
        Task<bool> HasPermissionAsync(List<string> roleIds, string? controllerName, string? actionName, string? areaName);

    }
}
