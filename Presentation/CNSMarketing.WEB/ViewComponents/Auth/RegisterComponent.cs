using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.ViewComponents.Auth
{
    [ViewComponent(Name = "Register")]
    public class RegisterComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
