using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
