using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.ViewComponents.Auth
{
    [ViewComponent(Name = "Login")]
    public class LoginComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
