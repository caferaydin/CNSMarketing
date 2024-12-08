using CNSMarketing.Application.Repositories;
using CNSMarketing.Application.ViewModels;
using CNSMarketing.Domain.Entities;
using CNSMarketing.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CNSMarketing.Persistence.Repositories.Manager
{
    public class MenuRolePermissionReadRepository : ReadRepository<MenuRolePermission, Guid>, IMenuRolePermissionReadRepository
    {
        private readonly CNSMarketingDbContext _context;
        public MenuRolePermissionReadRepository(CNSMarketingDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<NavigationMenuViewModel>> GetAllRolePermissionsAsync(string roleId)
        {
            var menuItems = await (from menu in _context.Menus
                                   join rolePermission in _context.MenuRolePermissions
                                   on menu.Id equals rolePermission.MenuId
                                   where rolePermission.RoleId == roleId && menu.IsActive
                                   select new NavigationMenuViewModel
                                   {
                                       Id = menu.Id,
                                       Name = menu.Name,
                                       ParentId = menu.ParentId,
                                       ControllerName = menu.ControllerName,
                                       ActionName = menu.ActionName,
                                       Area = menu.AreaName,
                                       DisplayOrder = menu.DisplayOrder,
                                       IsActive = menu.IsActive,
                                       Icon = menu.Icon
                                   })
                           .OrderBy(m => m.DisplayOrder)
                           .ToListAsync();

            return menuItems;

        }

        public async Task<bool> HasPermissionAsync(List<string> roleIds, string? controllerName, string? actionName, string? areaName)
        {
           
            if (roleIds == null || !roleIds.Any())
            {
                return false;
            }

            return await _context.MenuRolePermissions
                .AnyAsync(permission =>
                    roleIds.Contains(permission.RoleId) &&
                    (controllerName == null || permission.Menu.ControllerName == controllerName) &&
                    (actionName == null || permission.Menu.ActionName == actionName) &&
                    (areaName == null || permission.Menu.AreaName == areaName));
        }

        public async Task<List<string>> GetRoleIdsByUserId(string userId)
        {
            return await _context.UserRoles
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .ToListAsync();
        }

        public async Task<List<NavigationMenuViewModel>> GetAllPermissionsForRolesAsync(IEnumerable<string> roleIds)
        {
            // Tüm rollerin izinlerini tek seferde çekiyoruz
            var menuItems = await (from menu in _context.Menus
                                   join rolePermission in _context.MenuRolePermissions
                                   on menu.Id equals rolePermission.MenuId
                                   where roleIds.Contains(rolePermission.RoleId) && menu.IsActive
                                   select new NavigationMenuViewModel
                                   {
                                       Id = menu.Id,
                                       Name = menu.Name,
                                       ParentId = menu.ParentId,
                                       ControllerName = menu.ControllerName,
                                       ActionName = menu.ActionName,
                                       Area = menu.AreaName,
                                       DisplayOrder = menu.DisplayOrder,
                                       IsActive = menu.IsActive,
                                       Icon = menu.Icon
                                   })
                           .OrderBy(m => m.DisplayOrder)
                           .ToListAsync();

            return menuItems;
        }

    }
}
