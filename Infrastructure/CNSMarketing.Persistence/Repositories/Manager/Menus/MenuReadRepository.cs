using CNSMarketing.Application.Repositories;
using CNSMarketing.Application.ViewModels;
using CNSMarketing.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using CNSMarketing.Domain.Entities;

namespace CNSMarketing.Persistence.Repositories.Manager
{
    public class MenuReadRepository : ReadRepository<Menu, Guid>, IMenuReadRepository
    {
        private readonly CNSMarketingDbContext _context;
        public MenuReadRepository(CNSMarketingDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Menu>> GetAllMenusAsync()
        {
            var menus = await _context.Menus
                .Where(x => x.IsActive == true)
                .ToListAsync();

            return menus;
        }
        public async Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string roleId)
        {
            var menuItems = await (from menu in _context.Menus
                                   join rolePermission in _context.MenuRolePermissions
                                   on menu.Id equals rolePermission.MenuId
                                   where rolePermission.RoleId == roleId && menu.IsActive
                                   select new NavigationMenuViewModel
                                   {
                                       Id = menu.Id,
                                       Name = menu.Name,
                                       ParentId = menu.ParentId, // Güncellenmiş özellik
                                       ControllerName = menu.ControllerName,
                                       ActionName = menu.ActionName,
                                       Area = menu.AreaName,
                                       Permitted = menu.Permitted,
                                       DisplayOrder = menu.DisplayOrder,
                                       IsActive = menu.IsActive,
                                       Icon = menu.Icon // Yeni eklenen özellik
                                   }).OrderBy(m => m.DisplayOrder)
                                   .ToListAsync();

            return menuItems;
        }

    }
}
