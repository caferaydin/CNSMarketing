using CNSMarketing.WEB.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CNSMarketing.WEB.Areas.Manager.Controllers
{
    [Area("Manager")]
    [DynamicAuthorization]
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
