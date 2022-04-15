using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
