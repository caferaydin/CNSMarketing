using CNSMarketing.Application.Abstraction.Service.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CNSMarketing.WEB.Filters
{
    public class DynamicAuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string? _controllerName;
        private readonly string? _actionName;
        private readonly string? _areaName;

        public DynamicAuthorizationAttribute(string? controllerName = null, string? actionName = null, string? areaName = null)
        {
            _controllerName = controllerName;
            _actionName = actionName;
            _areaName = areaName;
        }


        private static readonly HashSet<(string Controller, string Action)> SpecialPages = new HashSet<(string, string)>
        {
            ("Auth", "Login"),
            ("Users", "Register"),
            ("Users", "Profile"),
            ("Auth", "Logout")
        };

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return;
            }

            var routeData = context.ActionDescriptor.RouteValues;
            var currentController = routeData["controller"];
            var currentAction = routeData["action"];
            //var currentArea = _areaName ?? routeData["area"];

            // Menüleri dinamik olarak çek
            var menuService = httpContext.RequestServices.GetRequiredService<IMenuService>();
            var menus = await menuService.GetMenusAsync();
            var isMenuPage = menus.Any(menu => menu.ControllerName == currentController && menu.ActionName == currentAction);

            if (!isMenuPage)
            {
                return;
            }

            //if (IsSpecialPage(currentController, currentAction))
            //{
            //    return;
            //}

            var navigationMenuService = httpContext.RequestServices.GetRequiredService<INavigationMenuService>();
            var hasPermission = await navigationMenuService.HasPermissionAsync(user, currentController, currentAction, _areaName);


            //if (!MenuPages.IsMenuPage(currentController, currentAction))
            //{
            //    return;
            //}

            if (!hasPermission)
            {
                // Hata mesajını ViewData ile hata sayfasına gönder
                var viewResult = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState)
                {
                    { "ErrorMessage", "Bu işlemi gerçekleştirmek için yetkiniz yok." }
                }
                };

                context.Result = viewResult;
            }
        }

        private bool IsSpecialPage(string controller, string action)
        {
            return SpecialPages.Contains((controller, action));
        }

    }
}
