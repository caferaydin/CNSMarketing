using CNSMarketing.Application.Abstraction.Service.Manager;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.ViewComponents.ManagerLayout
{
    [ViewComponent(Name = "NavigationMenu")]
    public class NavigationMenuComponent : ViewComponent
    {
        private readonly INavigationMenuService _navigationMenuService;
        private readonly IMenuService _menuService; 

        public NavigationMenuComponent(INavigationMenuService navigationMenuService, IMenuService menuService)
        {
            _navigationMenuService = navigationMenuService;
            _menuService = menuService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext.User; // Kullanıcı bilgilerini al

            // Yorum satırı ile rol bazlı kontrol örneği

                var menuItems = await _navigationMenuService.GetMenuItemsAsync(user);

            //if (user.IsInRole("Admin")) // Eğer kullanıcı Admin rolündeyse
            //{
            //    var items =  await _menuService.GetMenusAsync(); // Sadece admin menü öğelerini göster

            //    return View(items);
            //} 

            return View(menuItems); // Menü öğelerini View'a ilet

        }
    }
}
