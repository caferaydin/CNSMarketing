using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.Areas.SocialMedia.Controllers
{
    [Area("SocialMedia")]
    public class InstagramController : Controller
    {


        public InstagramController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }




    }
}
