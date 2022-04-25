using GameReviewSite.Core.Constants;
using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReviewSite.Controllers
{
    public class ForumMessageController :BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManagerService;
        private readonly IHttpContextAccessor whatever;
        private readonly IForumMessageService forumMessageService;


        public ForumMessageController(IUserService userService, 
            UserManager<ApplicationUser> userManagerService, IForumMessageService forumMessageService, IHttpContextAccessor whatever)
        {
            this.userService = userService;
            this.userManagerService = userManagerService;
            this.forumMessageService = forumMessageService;
            this.whatever = whatever;
        }

        [HttpGet]
        public async Task<IActionResult> AddMessage()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMessage(ForumMessage model)
        {
            ClaimsPrincipal? userContext = whatever.HttpContext?.User;
            ApplicationUser? user = await userManagerService.GetUserAsync(userContext);

            if (await forumMessageService.AddForumMessage(model,user.Id))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(AllMessages));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }


        public async Task<IActionResult> AllMessages()
        {
            var messages = await forumMessageService.GetMessages();
            return View(messages);
        }
    }
}
