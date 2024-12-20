﻿using CNSMarketing.Application.Repositories;
using CNSMarketing.Application.ViewModels;
using CNSMarketing.Domain.Entities;
using CNSMarketing.Application.Abstraction.Service.Manager;
using System.Security.Claims;
using CNSMarketing.Application.Abstraction.ExternalService.Common;

namespace CNSMarketing.Persistence.Service.Manager
{
    public class NavigationMenuService : GenericService<MenuRolePermission, Guid, IMenuRolePermissionReadRepository, IMenuRolePermissionWriteRepository>, INavigationMenuService
    {
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuRolePermissionReadRepository _menuRolePermissionReadRepository;
        private readonly IMenuRolePermissionWriteRepository _menuRolePermissionWriteRepository;
        private readonly ICacheService _cacheService;

        public NavigationMenuService(IMenuRolePermissionReadRepository readRepository, IMenuRolePermissionWriteRepository writeRepository, IMenuReadRepository menuReadRepository, ICacheService cacheService) : base(readRepository, writeRepository)
        {
            _menuRolePermissionReadRepository = readRepository;
            _menuRolePermissionWriteRepository = writeRepository;
            _menuReadRepository = menuReadRepository;
            _menuReadRepository = menuReadRepository;
            _cacheService = cacheService;
        }

        //public async Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal)
        //{

        //    var roleIds = await GetUserRoleIdsAsync(principal);
        //    var permittedMenuItems = new List<NavigationMenuViewModel>();


        //    foreach (var roleId in roleIds)
        //    {
        //        var roleSpecificMenus = await _menuRolePermissionReadRepository.GetAllRolePermissionsAsync(roleId);

        //        permittedMenuItems.AddRange(roleSpecificMenus);
        //    }

        //    var filteredMenus = permittedMenuItems
        //        .Where(menu => menu.IsActive)
        //        .OrderBy(x => x.DisplayOrder)
        //        .ToList();

        //    return BuildMenuHierarchy(filteredMenus);
        //}

        public async Task<List<NavigationMenuViewModel>> GetMenuItemsAsync(ClaimsPrincipal principal)
        {
            // Kullanıcının rollerini al
            var roleIds = await GetUserRoleIdsAsync(principal);

            // Roller için izinleri tek bir sorguda al
            var permittedMenuItems = await _menuRolePermissionReadRepository.GetAllPermissionsForRolesAsync(roleIds);

            // Menü hiyerarşisini oluştur
            var filteredMenus = permittedMenuItems
                .Where(menu => menu.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            return BuildMenuHierarchy(filteredMenus);
        }



        public async Task<List<NavigationMenuViewModel>> GetPermissionsByRoleIdAsync(string? id)
        {
            return await _menuReadRepository.GetPermissionsByRoleIdAsync(id);
        }

        public async Task<bool> SetPermissionsByRoleIdAsync(string? id, IEnumerable<Guid> permissionIds)
        {
            return await _menuRolePermissionWriteRepository.SetPermissionsByRoleIdAsync(id, permissionIds);
        }


        private async Task<List<string>> GetUserRoleIdsAsync(ClaimsPrincipal ctx)
        {
            var userId = GetUserId(ctx);
            return await _menuRolePermissionReadRepository.GetRoleIdsByUserId(userId);
        }

        private string? GetUserId(ClaimsPrincipal user)
        {
            return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string? controllerName, string? actionName, string? areaName)
        {
            var roleIds = await GetUserRoleIdsAsync(user);

            return await _menuRolePermissionReadRepository.HasPermissionAsync(roleIds, controllerName, actionName, areaName);

        }


        //private List<NavigationMenuViewModel> BuildMenuHierarchy(List<NavigationMenuViewModel> menuItems)
        //{
        //    // Menülerin ID'lerini anahtar olarak kullanan bir sözlük oluştur
        //    var menuDictionary = menuItems.ToDictionary(m => m.Id);

        //    // Kök menüleri tutacak liste
        //    var rootMenus = new List<NavigationMenuViewModel>();

        //    // Her menü için
        //    foreach (var menu in menuItems)
        //    {
        //        // Alt menü olup olmadığını kontrol et
        //        if (menu.ParentId.HasValue && menuDictionary.TryGetValue(menu.ParentId.Value, out var parentMenu))
        //        {
        //            // Eğer alt menü varsa, SubMenus listesine ekle
        //            if (parentMenu.SubMenus == null)
        //            {
        //                parentMenu.SubMenus = new List<NavigationMenuViewModel>();
        //            }
        //            parentMenu.SubMenus.Add(menu);
        //        }
        //        else
        //        {
        //            // Kök menü olarak ekle
        //            rootMenus.Add(menu);
        //        }
        //    }

        //    return rootMenus;
        //}

        private List<NavigationMenuViewModel> BuildMenuHierarchy(List<NavigationMenuViewModel> menuItems)
        {
            // Menülerin ID'lerini anahtar olarak kullanan bir sözlük oluştur
            var menuDictionary = menuItems.ToDictionary(m => m.Id);

            // Kök menüleri tutacak liste
            var rootMenus = new List<NavigationMenuViewModel>();

            foreach (var menu in menuItems)
            {
                if (menu.ParentId.HasValue && menuDictionary.TryGetValue(menu.ParentId.Value, out var parentMenu))
                {
                    // Alt menüleri ekle
                    AddToParentMenu(parentMenu, menu);
                }
                else
                {
                    // Kök menü olarak ekle
                    rootMenus.Add(menu);
                }
            }

            return rootMenus;
        }

        // Alt menüleri üst menüye ekleyen yardımcı metot
        private void AddToParentMenu(NavigationMenuViewModel parentMenu, NavigationMenuViewModel childMenu)
        {
            if (parentMenu.SubMenus == null)
            {
                parentMenu.SubMenus = new List<NavigationMenuViewModel>();
            }
            parentMenu.SubMenus.Add(childMenu);
        }





    }
}
