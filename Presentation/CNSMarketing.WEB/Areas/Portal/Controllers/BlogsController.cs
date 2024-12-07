using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.Areas.Portal.Controllers
{
    [Area("Portal")]
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
