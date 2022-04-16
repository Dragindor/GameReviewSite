using GameReviewSite.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleConstants.Roles.Admin)]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
