using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameReviewSite.Core.Constants;

namespace GameReviewSite.Areas.Admin.Controllers
{
    [Authorize]
    //[Authorize(Roles = RoleConstants.Roles.Admin)]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
