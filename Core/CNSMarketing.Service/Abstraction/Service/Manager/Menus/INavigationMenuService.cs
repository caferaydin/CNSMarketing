using CNSMarketing.Application.Repositories;
using CNSMarketing.Application.ViewModels;
using System.Security.Claims;
using CNSMarketing.Domain.Entities;

namespace CNSMarketing.Application.Abstraction.Service.Manager
{
    public interface INavigationMenuService : IGenericService<MenuRolePermission,Guid,IMenuRolePermissionReadRepository, IMenuRolePermissionWriteRepository>
    {
        //Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string? ctrl, string? act, string? area);
        Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal);
        Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string? id);
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string? controllerName, string? actionName, string? areaName);
        Task<bool> SetPermissionsByRoleIdAsync(string? id, IEnumerable<Guid> permissionIds);
    }
}
