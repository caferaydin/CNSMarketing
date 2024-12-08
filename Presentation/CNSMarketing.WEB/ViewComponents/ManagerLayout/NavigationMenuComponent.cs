using CNSMarketing.Application.Abstraction.Service.Manager;
using CNSMarketing.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CNSMarketing.WEB.ViewComponents.ManagerLayout
{

    [ViewComponent(Name = "NavigationMenu")]
    public class NavigationMenuComponent : ViewComponent
    {
        private readonly INavigationMenuService _navigationMenuService;
        private readonly IDistributedCache _cache;

        public NavigationMenuComponent(INavigationMenuService navigationMenuService, IDistributedCache cache)
        {
            _navigationMenuService = navigationMenuService;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext.User; // Kullanıcı bilgilerini al
            var cacheKey = $"NavigationMenu_{user.Identity?.Name}";

            // Önce cache kontrolü yap
            var cachedMenu = await _cache.GetStringAsync(cacheKey);
            List<NavigationMenuViewModel> menuItems;

            if (!string.IsNullOrEmpty(cachedMenu))
            {
                // Cache'den menüleri getir
                menuItems = System.Text.Json.JsonSerializer.Deserialize<List<NavigationMenuViewModel>>(cachedMenu);
            }
            else
            {
                // Cache'de yoksa back-end'e gidip menüleri getir
                menuItems = await _navigationMenuService.GetMenuItemsAsync(user);

                // Cache'e ekle (10 dakika geçerli)
                var serializedMenu = System.Text.Json.JsonSerializer.Serialize(menuItems);
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };
                await _cache.SetStringAsync(cacheKey, serializedMenu, cacheOptions);
            }

            return View(menuItems);
        }
    }


    //[ViewComponent(Name = "NavigationMenu")]
    //public class NavigationMenuComponent : ViewComponent
    //{
    //    private readonly INavigationMenuService _navigationMenuService;
    //    private readonly IMenuService _menuService;

    //    public NavigationMenuComponent(INavigationMenuService navigationMenuService, IMenuService menuService)
    //    {
    //        _navigationMenuService = navigationMenuService;
    //        _menuService = menuService;
    //    }


    //    public async Task<IViewComponentResult> InvokeAsync()
    //    {
    //        var user = HttpContext.User;

    //        var menuItems = await _navigationMenuService.GetMenuItemsAsync(user);

    //        return View(menuItems); // Menü öğelerini View'a ilet

    //    }
    //}
}
